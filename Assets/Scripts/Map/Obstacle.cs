using System;
using UnityEngine;

namespace Map
{
    public class Obstacle : MonoBehaviour
    {
        public Vector2[] GetCorners()
        {
            var bounds = GetComponent<Collider2D>().bounds;

            return new Vector2[]
            {
                new Vector2(bounds.min.x, bounds.min.y),
                new Vector2(bounds.max.x, bounds.min.y),
                new Vector2(bounds.max.x, bounds.max.y),
                new Vector2(bounds.min.x, bounds.max.y)
            };
        }
    }
}
