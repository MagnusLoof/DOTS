using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class WeaponAuthoring : MonoBehaviour
{
    public float RotationSpeed;
    public GameObject ProjectilePrefab;
    public float3 MousePosition;

    class WeaponBake : Baker<WeaponAuthoring>
    {
        public override void Bake(WeaponAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<WeaponTag>(entity);
            AddComponent<WeaponRotationInput>(entity);
            AddComponent(entity, new WeaponRotationSpeed
            {
                Value = authoring.RotationSpeed,
            });
            
            AddComponent<FireProjectileTag>(entity);
            SetComponentEnabled<FireProjectileTag>(entity, false);
            AddComponent(entity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
            });
            
            // AddComponent<MousePosition>(entity, new MousePosition
            // {
            //     Value = authoring.MousePosition,
            // });
        }
    }
}

public struct WeaponTag : IComponentData { }

public struct WeaponRotationInput : IComponentData
{
    public float Value;
}

public struct WeaponRotationSpeed : IComponentData
{
    public float Value;
}

public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

public struct ProjectileSpeed : IComponentData
{
    public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }

// public struct MousePosition : IComponentData
// {
//     public float3 Value;
// }

