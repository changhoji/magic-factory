using Unity.Entities;
using Unity.Mathematics;
using Components;

namespace Aspects
{
    public readonly partial struct FactoryAspect : IAspect
    {
        private readonly RefRW<Factory> _factory;

        private float ProduceInterval => _factory.ValueRO.ProduceInterval;

        public float3 ProducePosition => _factory.ValueRO.ProducePosition;

        private float NextProductTime
        {
            get => _factory.ValueRO.NextProductTime;
            set => _factory.ValueRW.NextProductTime = value;
        }

        public bool CanProduce(double elapsedTime) => NextProductTime < elapsedTime;
        
        public void UpdateNextProduceTime(double elapsedTime) => NextProductTime = (float)elapsedTime + ProduceInterval;
    }

    public readonly partial struct MinerAspect : IAspect
    {
        private readonly FactoryAspect _factory;
        private readonly RefRO<Miner> _miner;
        
        public FactoryAspect Factory => _factory;
        
        public Entity ProductPrefab => _miner.ValueRO.ProductPrefab;
    }
}