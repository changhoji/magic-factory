using Components;
using Systems;
using Unity.Entities;
using UnityEngine;

namespace Authorings
{
    public class ElementTileAuthoring : MonoBehaviour
    {
        public ElementType Type;
        private class ElementTileAuthoringBaker : Baker<ElementTileAuthoring>
        {
            public override void Bake(ElementTileAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent(entity, new ElementTile
                {
                    Type = authoring.Type,
                });
            }
        }
    }
}