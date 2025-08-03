using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public partial struct PlayerInputSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MinerPrefab>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            if (Input.GetMouseButton(0))
            {
                var entity = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponentData(entity, new BuildRequest
                {
                    Prefab = SystemAPI.GetSingleton<MinerPrefab>().Prefab,
                    WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                });
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}