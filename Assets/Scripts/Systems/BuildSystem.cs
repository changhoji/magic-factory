using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct BuildSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = GetEntityCommandBuffer(ref state);
            var gridSystemHandle = state.World.GetExistingSystem<GridMapSystem>();
            ref var gridSystem = ref state.World.Unmanaged.GetUnsafeSystemRef<GridMapSystem>(gridSystemHandle);

            new BuildJob
            {
                Ecb = ecb,
                Buildings = gridSystem.Buildings.AsParallelWriter(),
            }.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            return ecb.AsParallelWriter();
        }
    }

    [BurstCompile]
    public partial struct BuildJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        public NativeParallelHashMap<int2, Entity>.ParallelWriter Buildings;

        private void Execute(Entity entity, in BuildRequest buildRequest)
        {
            int2 gridPosition = GetGridPosition(buildRequest.WorldPosition);
            float3 position = new float3(gridPosition.x, gridPosition.y, 0);
            
            Debug.Log($"mousePosition = {buildRequest.WorldPosition}, gridPosition = {gridPosition}");

            Entity newEntity = Ecb.Instantiate(0, buildRequest.Prefab);
            
            Ecb.DestroyEntity(0, entity);
            bool success = Buildings.TryAdd(gridPosition, newEntity);
            if (!success)
            {
                Ecb.DestroyEntity(0, newEntity);
                return;
            }
            Ecb.SetComponent(0, newEntity, LocalTransform.FromPosition(position));
            Ecb.SetComponent(0, newEntity, new Factory
            {
                ProduceInterval = 1f,
                NextProductTime = 0f,
                ProducePosition = position,
            });
        }
        
        private int2 GetGridPosition(float3 worldPosition)
        {
            return new int2(Mathf.FloorToInt(worldPosition.x + .5f), Mathf.FloorToInt(worldPosition.y + .5f));
        }
    }
}