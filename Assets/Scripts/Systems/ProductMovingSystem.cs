using Components;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;

namespace Systems
{
    [BurstCompile]
    public partial struct ProductMovingSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new ProductMovingJob
            {
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct ProductMovingJob : IJobEntity
    {
        private void Execute(ref LocalTransform transform, in Product product)
        {
            transform.Position += new float3(0.1f, 0, 0);
        }
    }
}