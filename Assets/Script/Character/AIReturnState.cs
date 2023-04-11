using System;
using UnityEngine;
namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        Vector3 targetPos;

        public override void EnterState(EnemyController enemy)
        {
            enemy.movementComponent.UpdateAgentMovementSpeed(enemy.stats.walkSpeed);
            if(enemy.patrolComponent != null)
            {
                targetPos = enemy.patrolComponent.GetNextPosition();
                enemy.movementComponent.MoveAgentByDestination(targetPos);
            }
            else
            {
                enemy.movementComponent.MoveAgentByDestination(enemy.originalPos);
            }
        }
        public override void UpdateState(EnemyController enemy)
        {
            //Transitioning between states
            if(enemy.distanceFromPlayer < enemy.rangeRadius)
            {
                enemy.SwitchState(enemy.chasingState);
                return;
            }
            if(enemy.movementComponent.ReachedDestination())
            {
                if (enemy.patrolComponent != null)
                {
                    enemy.SwitchState(enemy.patrolState);
                    return;
                } 
                else
                {
                    enemy.movementComponent.RotatePlayer(enemy.movementComponent.originalForwardVector);
                }
            }
            else
            {
                if(enemy.patrolComponent != null)
                {
                    Vector3 newForwardVector = targetPos - enemy.transform.position;
                    newForwardVector.y = 0;
                    enemy.movementComponent.RotatePlayer(newForwardVector);
                }
                else
                {
                    Vector3 newForwardVector = enemy.originalPos - enemy.transform.position;
                    newForwardVector.y = 0;
                    enemy.movementComponent.RotatePlayer(newForwardVector);
                }
            }
        }
    }
}