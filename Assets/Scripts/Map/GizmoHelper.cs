using UnityEngine;

namespace Map
{
    public static class GizmoHelper
    {
        public static void DrawCircle(Vector3 center, float radius, int segments)
        {
            var angle = 2 * Mathf.PI / segments;

            var startPoint = center + new Vector3(radius, 0, 0);
            var previousPoint = startPoint;

            for (var i = 1; i <= segments; i++)
            {
                var x = Mathf.Cos(i * angle) * radius;
                var y = Mathf.Sin(i * angle) * radius;

                var nextPoint = center + new Vector3(x, y, 0);
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
            
            Gizmos.DrawLine(previousPoint, startPoint);
        }
    }
}
