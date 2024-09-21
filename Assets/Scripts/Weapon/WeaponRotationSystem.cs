using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct WeaponRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new WeaponRotationJob
        {
        }.Schedule();
    }
}


public partial struct WeaponRotationJob : IJobEntity
{
    private void Execute(ref LocalTransform transform, in WeaponTag weaponTag)
    {
        Vector3 direction = CameraManager.Instance.mousePosition - CameraManager.Instance.cameraPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion directionalRotation = Quaternion.Euler(0, 0, angle);
        transform.Rotation = directionalRotation;
    }
}