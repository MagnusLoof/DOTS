using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition; // Better Vector2
    public float NextSpawnTime;
    public float SpawnRate;
}

