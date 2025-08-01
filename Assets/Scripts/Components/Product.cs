using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct Product : IComponentData
    {
        public Entity Prefab;
        public float3 SpawnPosition;
    }
}