using Components;
using UnityEngine;
using Unity.Entities;

namespace Authorings
{
    public class MinerAuthoring : MonoBehaviour
    {
        public GameObject prefab;
    }

    class MinerBaker : Baker<MinerAuthoring>
    {
        public override void Bake(MinerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            AddComponent(entity, new Miner
            {
                ProductPrefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
            });
            AddComponent(entity, new Factory
            {
                ProduceInterval = 1f,
                ProducePosition = authoring.transform.position,
                NextProductTime = 0.0f,
            });
        }
    }
}
