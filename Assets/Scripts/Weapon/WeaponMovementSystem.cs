using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct WeaponMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerPosition>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float3 playerPosition = SystemAPI.GetSingleton<PlayerPosition>().Value;
        new WeaponMoveJob
        {
            PlayerPosition = playerPosition
        }.Schedule();
    }
}

[BurstCompile]
public partial struct WeaponMoveJob : IJobEntity
{
    public float3 PlayerPosition; // Add this line

    [BurstCompile]
    private void Execute(ref LocalTransform transform, in ProjectilePrefab prefab)
    {
        // Update the weapon's position to be the same as the player's position
        //transform.Position = PlayerPosition;
    }
}