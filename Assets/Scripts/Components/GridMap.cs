using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct GridMap : IComponentData
    {
        public int Width;
        public int Height;
    }
}