using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class EnemySpawnerSystem : SystemBase
{
    private float timer = 0;
    private const float SPAWN_INTERVAL = 1;
    private EnemyEntity _enemyEntity;
    private Entity _playerEntity;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<EnemyEntity>();
    }
    
    protected override void OnStartRunning()
    {
        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        _enemyEntity = SystemAPI.GetSingleton<EnemyEntity>();
    }
    
    protected override void OnUpdate()
    {
        var playerPos = SystemAPI.GetComponent<LocalToWorld>(_playerEntity).Position;
        if (timer >= SPAWN_INTERVAL)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(World.Unmanaged);
            SpawnNextWave(playerPos, ref ecb);
            timer = 0;
        }
        else
        {
            timer += SystemAPI.Time.DeltaTime;
        }
    }
    
    private void SpawnNextWave(float3 playerPos, ref EntityCommandBuffer ecb)
    {
        var newEnemy = ecb.Instantiate(_enemyEntity.RedEnemy);
        ecb.SetComponent(newEnemy, LocalTransform.FromPosition(new float3(10, 10,0)));
        ecb.AddComponent<EnemyTag>(newEnemy);
        // var enemyPrefab = GetSingleton<EnemyEntity>().RedEnemy;
        // var enemyEntity = ecb.Instantiate(enemyPrefab);
        // ecb.SetComponent(enemyEntity, new Translation { Value = playerPos });
    }
}
