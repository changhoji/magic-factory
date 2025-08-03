using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct BuildRequest : IComponentData
    {
        public Entity Prefab;
        public float3 WorldPosition;
    }
}