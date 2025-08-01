using Components;
using UnityEngine;
using Unity.Entities;

namespace Authorings
{
    public class ProductAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
    }

    class ProductBaker : Baker<ProductAuthoring>
    {
        public override void Bake(ProductAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Product()
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.transform.position,
            });
        }
    }
}