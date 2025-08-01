using Components;
using Unity.Entities;
using UnityEngine;

namespace Authorings
{
    public class GridConfigAuthoring : MonoBehaviour
    {
        public int Width;
        public int Height;

        private class GridConfigBaker : Baker<GridConfigAuthoring>
        {
            public override void Bake(GridConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new GridConfig
                {
                    Width = authoring.Width,
                    Height = authoring.Height,
                });
            }
        }
    }
}