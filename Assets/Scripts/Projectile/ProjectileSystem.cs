using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct ProjectileSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerPosition>();
    }

    public void OnUpdate(ref SystemState state)
    {
        // ECB is an array of commands that can be played back later
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        float3 playerPosition = SystemAPI.GetSingleton<PlayerPosition>().Value;
        foreach (var (projectilePrefab, transform) in SystemAPI.Query<ProjectilePrefab, LocalTransform>()
                     .WithAll<FireProjectileTag>())
        {
            var newProjectile = ecb.Instantiate(projectilePrefab.Value);
            var projectileTransform = LocalTransform.FromPositionRotation(playerPosition, transform.Rotation);
            ecb.SetComponent(newProjectile, projectileTransform);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
