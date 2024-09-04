using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct WeaponRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new WeaponRotationJob
        {
        }.Schedule();
    }
}


public partial struct WeaponRotationJob : IJobEntity
{
    private void Execute(ref LocalTransform transform, in WeaponRotationInput input, WeaponRotationSpeed speed, in MousePosition mousePosition)
    {
        Vector3 direction = CameraManager.Instance.MousePosition - (Vector3)transform.Position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion directionalRotation = Quaternion.Euler(0, 0, angle);
        transform.Rotation = directionalRotation;
    }
}