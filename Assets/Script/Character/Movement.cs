using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Collections;
using RPG.Utility;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float playerSpeedSprint = 2.5f;
        [SerializeField] private float playerStamina = 10f;
        [SerializeField] private float staminaRegenerationRate = 1f;
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
            if(CompareTag(Constants.PLAYER_TAG)) RotatePlayer(movementVector);
        }

        public void RotatePlayer(Vector3 newPosition)
        {
            if(newPosition == Vector3.zero) return;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newPosition);
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
            if (isShiftPressed && playerStamina > 0)
            {
                offset *= playerSpeedSprint;
                //stamina
                playerStamina -= 2f * Time.deltaTime;
                if(playerStamina < 0f)
                {
                    playerStamina = 0f;
                }
            }
            else
            {
                StartCoroutine(RegenerateStamina());
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
        private IEnumerator RegenerateStamina()
        {
            while (!isShiftPressed && playerStamina < 10f)
            {
                playerStamina += staminaRegenerationRate * Time.deltaTime;
                yield return null;
            }
        }
        public void UpdateAgentMovementSpeed(float newSpeed)
        {
            navMeshAgent.speed = newSpeed; 
        }
    }
}