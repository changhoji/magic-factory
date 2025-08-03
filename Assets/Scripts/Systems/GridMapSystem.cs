using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    public partial struct GridMapSystem : ISystem
    {
        [NativeDisableParallelForRestriction]
        public NativeParallelHashMap<int2, Entity> Buildings;
        
        public void OnCreate(ref SystemState state)
        {
            Buildings = new NativeParallelHashMap<int2, Entity>(1000, Allocator.Persistent);
        }

        public void OnUpdate(ref SystemState state)
        {
        }
    }
}
