using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
public partial struct EnemyMovementSystem : ISystem, ISystemStartStop
{
    private Entity playerEntity;
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }
    
    public void OnStartRunning(ref SystemState state)
    {
        playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    public void OnStopRunning(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
        new EnemyMoveJob
        {
            DeltaTime = deltaTime,
            PlayerPosition = playerPosition
        }.ScheduleParallel();
    }
    
    [BurstCompile]
    [WithAll(typeof(EnemyTag))]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float3 PlayerPosition;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in EnemyTag enemyTag)
        {
            var directionToPlayer = math.normalizesafe(PlayerPosition - transform.Position);
            transform.Position += directionToPlayer * 5.0f * DeltaTime;
        }
    }
}
