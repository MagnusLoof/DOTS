using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float RotationSpeed;
    public GameObject ProjectilePrefab;

    class PlayerBake : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerMoveInput>(entity);
            AddComponent<PlayerRotationInput>(entity);

            AddComponent(entity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed,
            });
            
            AddComponent(entity, new PlayerRotationSpeed
            {
                Value = authoring.RotationSpeed,
            });

            AddComponent<FireProjectileTag>(entity);
            SetComponentEnabled<FireProjectileTag>(entity, false);

            AddComponent(entity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}

public struct PlayerRotationInput : IComponentData
{
    public float Value;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

public struct PlayerRotationSpeed : IComponentData
{
    public float Value;
}

public struct PlayerTag : IComponentData { }

public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

public struct ProjectileSpeed : IComponentData
{
    public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }