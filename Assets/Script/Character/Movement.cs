using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float playerSpeedSprint = 1.5f;
        private Vector3 movementVector;
        private NavMeshAgent navMeshAgent;
        private bool isShiftPressed;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if(movementVector == Vector3.zero) return;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);
            //transforming the player's position on z axis
            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                1
            );
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }
        public void ShiftBeingPressed(InputAction.CallbackContext context)
        {
            isShiftPressed = context.ReadValue<float>() > 0.5f;
        }
        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * navMeshAgent.speed;
            if(isShiftPressed)
            {
                offset *= playerSpeedSprint;
            }
            navMeshAgent.Move(offset);
        }
        public void MoveAgentByDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }
        public void StopMovingAgent()
        {
            navMeshAgent.ResetPath();
        }
        public bool ReachedDestination()
        {
            if(navMeshAgent.pathPending) return false;
            if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) return false;
            if(navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude != 0f) return false;
            return true;
        }
        public void MoveAgentByOffset(Vector3 offset)
        {
            navMeshAgent.Move(offset);
        }
    }
}