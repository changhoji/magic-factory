using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public partial struct BuildSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.Enabled = false;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            int2 gridPosition = GetMouseGridPosition();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        private int2 GetMouseGridPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
                return new int2(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.z));
            return int2.zero;
        }
    }
}