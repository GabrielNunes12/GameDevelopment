using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Character
{
    public class AIChasingState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementComponent.UpdateAgentMovementSpeed(enemy.stats.runSpeed);
        }
        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.distanceFromPlayer > enemy.rangeRadius)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }
            if (enemy.distanceFromPlayer <= enemy.attackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }
            enemy.movementComponent.MoveAgentByDestination(enemy.player.transform.position);
        }
    }

}
