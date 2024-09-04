using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
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
        CameraPosition = (Vector3)SystemAPI.GetSingleton<PlayerPosition>().Value;
    }
}