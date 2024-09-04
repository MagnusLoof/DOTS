using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;

    class PlayerBake : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerMoveInput>(entity);
            AddComponent<PlayerPosition>(entity);

            AddComponent(entity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed,
            });
        }
    }
}

public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}

public struct PlayerPosition : IComponentData
{
    public float3 Value;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

public struct PlayerTag : IComponentData { }