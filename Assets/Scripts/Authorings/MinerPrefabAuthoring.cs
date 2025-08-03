using Components;
using Unity.Entities;
using UnityEngine;

namespace Authorings
{
    public class MinerPrefabAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        private class MinerPrefabAuthoringBaker : Baker<MinerPrefabAuthoring>
        {
            public override void Bake(MinerPrefabAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MinerPrefab()
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.None),
                });
            }
        }
    }
}