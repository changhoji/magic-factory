using Authorings.Utilities;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public enum ElementType
    {
        Fire,
        Water,
    }
    [BurstCompile]
    public partial struct GridMapSystem : ISystem
    {
        public NativeHashMap<int2, ElementType> Elements;
        public NativeParallelHashMap<int2, Entity> Buildings;
        
        public void OnCreate(ref SystemState state)
        {
            Elements = new NativeHashMap<int2, ElementType>(1000, Allocator.Persistent);
            Buildings = new NativeParallelHashMap<int2, Entity>(1000, Allocator.Persistent);
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            foreach (var (element, transform) in SystemAPI.Query<RefRO<ElementTile>, RefRO<LocalTransform>>())
            {
                Elements.TryAdd(GridMapUtility.WorldToGrid(transform.ValueRO.Position), element.ValueRO.Type);
            }
        }
    }
}
