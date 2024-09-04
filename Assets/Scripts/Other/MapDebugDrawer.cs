using System;
using System.Collections.Generic;
using Data;
using Map;
using UnityEngine;

namespace Other
{
    public class MapDebugDrawer : MonoBehaviour
    {
        [SerializeField] private Npc npc;
        
        private void OnDrawGizmos()
        {
            if (!npc) return;
            
            foreach (var task in npc.Tasks)
            {
                Gizmos.color = Color.blue;
                GizmoHelper.DrawCircle(task.TaskLocation, 0.5f, 35);
            }
        }
    }
}
