using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolComponent.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.rangeRadius)
            {
                enemy.SwitchState(enemy.chasingState);
                return;
            }
            enemy.patrolComponent.CalculateNextPosition();
            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPosition = enemy.patrolComponent.GetNextPosition();
            Vector3 offset = newPosition - currentPosition;
            enemy.movementComponent.MoveAgentByOffset(offset);
        }
    }

}
