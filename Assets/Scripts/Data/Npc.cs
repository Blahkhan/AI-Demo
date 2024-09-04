using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Npc/Npc")]
    public class Npc : ScriptableObject
    {
        public string NpcName;
        public float WalkSpeed;
        public List<Task> Tasks;
    }
}
