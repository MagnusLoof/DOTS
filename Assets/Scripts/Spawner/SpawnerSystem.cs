using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

public partial struct SpawnerSystem : ISystem //,ISystemStartStop
{
    public void OnStartRunning(ref SystemState state)
    {
        foreach (RefRW<PlayerSpawner> spawner in SystemAPI.Query<RefRW<PlayerSpawner>>())
        {
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            float3 position = float3.zero;
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(position));
        }
    }

    public void OnCreate(ref SystemState state) { }
    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}
