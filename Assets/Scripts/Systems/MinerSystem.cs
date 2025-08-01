using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Aspects;

namespace Systems
{
    [BurstCompile]
    public partial struct MinerSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }
        
        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

            new ProcessMinerJob
            {
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                Ecb = ecb
            }.ScheduleParallel();
        }

        private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            return ecb.AsParallelWriter();
        }
    }

    [BurstCompile]
    public partial struct ProcessMinerJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        public double ElapsedTime;

        private void Execute([ChunkIndexInQuery] int chunkIndex, MinerAspect miner)
        {
            FactoryAspect factory = miner.Factory;
            
            if (factory.CanProduce(ElapsedTime))
            {
                Entity newEntity = Ecb.Instantiate(chunkIndex, miner.ProductPrefab);
                Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(factory.ProducePosition).WithScale(0.5f));

                factory.UpdateNextProduceTime(ElapsedTime);
            }
        }
    }
}
