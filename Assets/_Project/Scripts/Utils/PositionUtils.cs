using UnityEngine;

namespace Eternity.Utils
{
    public static class PositionUtils
    {
        public static Vector3 ToV3(this Vector2Int vector2) => new Vector3(vector2.x, vector2.y, 0);
    }
}