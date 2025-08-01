using Unity.Entities;

namespace Components
{
    public struct GridConfig : IComponentData
    {
        public int Width;
        public int Height;
    }
}