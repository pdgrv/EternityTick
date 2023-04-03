using System.Collections;
using UnityEngine;

namespace Eternity.Utils
{
    public static class PositionUtils
    {
        public static Vector3 ToV3(this Vector2Int vector2) => new Vector3(vector2.x, vector2.y, 0);

        public static IEnumerator LerpMoving(this Transform transform, Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            float timeElapsed = 0f;

            while (timeElapsed <= duration)
            {
                timeElapsed += Mathf.Min(timeElapsed + Time.deltaTime, duration);
                transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
                yield return null;
            }
        }
    }
}