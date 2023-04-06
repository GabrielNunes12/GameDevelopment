using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementComponent.StopMovingAgent();
        }
        public override void UpdateState(EnemyController enemy)
        {
            if(enemy.distanceFromPlayer < enemy.attackRange)
            {
                enemy.hitPlayer -= enemy.attackPower;
                return;
            } 
            else
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }
        }
    }
}

