using Unity.Mathematics;
using UnityEngine;

namespace Authorings.Utilities
{
    public static class GridMapUtility
    {
        public static int2 WorldToGrid(float3 worldPosition)
        {
            return new int2(Mathf.FloorToInt(worldPosition.x + 0.5f), Mathf.FloorToInt(worldPosition.y + 0.5f));
        }
    }
}