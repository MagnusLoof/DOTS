using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMoveSpeed speed, in PlayerRotationInput rotationInput, PlayerRotationSpeed rotationSpeed)
    {
        // Vector3 targetDir = new float3(input.Value.x, input.Value.y, 0);
        // targetDir.Normalize();
        // transform.Position += (float3)targetDir * speed.Value * DeltaTime;
        
        if (math.abs(rotationInput.Value) > 0)
        {
            // Assuming the rotation is around the Z-axis
            float rotationAngle = rotationInput.Value * rotationSpeed.Value * DeltaTime;
            quaternion currentRotation = transform.Rotation;
            quaternion rotationDelta = quaternion.Euler(0, 0, rotationAngle);
            transform.Rotation = math.mul(currentRotation, rotationDelta);
        }
    }
}