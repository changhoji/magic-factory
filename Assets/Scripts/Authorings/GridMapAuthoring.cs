using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authorings
{
    public class GridMapAuthoring : MonoBehaviour
    {
        public int Width;
        public int Height;

        private class GridConfigBaker : Baker<GridMapAuthoring>
        {
            public override void Bake(GridMapAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new GridMap
                {
                    Width = authoring.Width,
                    Height = authoring.Height,
                });
            }
        }
    }
}