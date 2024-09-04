using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Npc/Task")]
    public class Task : ScriptableObject
    {
        public Vector2 TaskLocation;
        public float TaskTime;
        public string TaskDoneMessage;
    }
}
