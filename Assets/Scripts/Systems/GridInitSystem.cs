using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct GridInitSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GridConfig>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

        }
    }
}
