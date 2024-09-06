using Unity.Entities;
using UnityEngine;

public class EnemyEntityAuthoring : MonoBehaviour
{
    public GameObject RedEnemy;
    public GameObject BlueEnemy;
    public GameObject TriangleEnemy;

    public class EnemyEntityBaker : Baker<EnemyEntityAuthoring>
    {
        public override void Bake(EnemyEntityAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemyEntity
            {
                RedEnemy = GetEntity(authoring.RedEnemy, TransformUsageFlags.Dynamic),
                BlueEnemy = GetEntity(authoring.BlueEnemy, TransformUsageFlags.Dynamic),
                TriangleEnemy = GetEntity(authoring.TriangleEnemy, TransformUsageFlags.Dynamic)
            });
            AddComponent<EnemyTag>(entity);
        }
    }
}
