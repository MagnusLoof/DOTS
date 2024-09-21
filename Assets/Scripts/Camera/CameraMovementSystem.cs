using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial class CameraMovementSystem : SystemBase
{
    public static CameraMovementSystem Instance;
    public Vector3 CameraPosition;
    
    protected override void OnCreate()
    {
        Instance = this;
    }

    protected override void OnUpdate()
    {
        CameraPosition = SystemAPI.GetSingleton<PlayerPosition>().Value;
    }
}