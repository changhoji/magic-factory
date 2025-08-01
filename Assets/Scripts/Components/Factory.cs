using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct Factory : IComponentData
    {
        public float ProduceInterval;
        public float NextProductTime;
        public float3 ProducePosition;
    }

    public struct Miner : IComponentData
    {
        public Entity ProductPrefab;
    }
}