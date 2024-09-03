using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct PlayerResetInputSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // ECB is an array of commands that can be played back later
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (tag, entity) in SystemAPI.Query<FireProjectileTag>()
                     .WithEntityAccess())
        {
            ecb.SetComponentEnabled<FireProjectileTag>(entity, false);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
