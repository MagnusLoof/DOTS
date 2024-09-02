using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity PreFab;
    public float2 SpawnPosition; // Better Vector2
    public float NextSpawnTime;
    public float SpawnRate;
}
