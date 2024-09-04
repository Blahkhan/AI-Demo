using System;
using System.Collections;
using Data;
using Map;
using UnityEngine;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Variables")]
        [SerializeField] private Npc npc;
        [SerializeField] private float avoidanceDistance;
        [SerializeField] private float stoppingDistance ;
        [SerializeField] private float taskDoingTime;
        
        [Header("Debug")] 
        [SerializeField] private bool drawGizmos;

        private int _taskCounter;
        private Task _currentTask;
        private Vector2 _avoidanceTarget;
        private bool _avoidingObstacle;
        private Vector2 _avoidPoint;
        
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;
            
            if (_currentTask) GizmoHelper.DrawCircle(_currentTask.TaskLocation, 1, 35);
            if (_avoidingObstacle) GizmoHelper.DrawCircle(_avoidanceTarget, 1, 35);
        }

        private void Start()
        {
            _taskCounter = 0;
            _currentTask = npc.Tasks[_taskCounter];
        }

        private void Update()
        {
            ProcessMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var objectCorners = other.GetComponent<Obstacle>();
            if (!objectCorners) return;
            
            _avoidanceTarget = CalculateAvoidanceTarget(objectCorners.GetCorners());
            _avoidingObstacle = true;
        }

        private void ProcessMovement()
        {
            if (_avoidingObstacle)
            {
                MoveTowards(_avoidanceTarget);

                if (Vector2.Distance(rb.position, _avoidanceTarget) < 0.1f)
                    _avoidingObstacle = false;
            }
            else
            {
                if (_currentTask)
                    MoveTowards(_currentTask.TaskLocation);
                else
                    rb.velocity = Vector2.zero;
            }
        }

        private void MoveTowards(Vector2 destination)
        {
            var direction = (destination - rb.position).normalized;
            rb.velocity = direction * npc.WalkSpeed;
            if (Vector2.Distance(rb.position, _currentTask.TaskLocation) < 0.1f)
                StartCoroutine(WaitForTaskEnd());
        }

        private void GetNewTask()
        {
            _taskCounter++;
            if (_taskCounter >= npc.Tasks.Count) _taskCounter = 0;
            
            _currentTask = npc.Tasks[_taskCounter];
        }

        private IEnumerator WaitForTaskEnd()
        {
            Debug.Log($"{npc.NpcName} {_currentTask.TaskDoneMessage}!");
            _currentTask = null;
            yield return new WaitForSeconds(taskDoingTime);
            GetNewTask();
        }

        private Vector2 CalculateAvoidanceTarget(Vector2[] corners)
        {
            var obstacleCenter = Vector2.zero;
            foreach (var corner in corners)
            {
                obstacleCenter += corner;
            }
            
            obstacleCenter /= corners.Length;
            var avoidanceDirection = (rb.position - obstacleCenter).normalized;
            
            return rb.position + avoidanceDirection * avoidanceDistance;
        }
    }
}
