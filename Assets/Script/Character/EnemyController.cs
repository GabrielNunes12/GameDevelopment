using UnityEngine;
using RPG.Utility;
using System;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public GameObject player;

        public float rangeRadius = 2.5f;
        public float attackRange = 0.7f;
        public float attackPower = 1f;
        public float hitPlayer = 100f;
        public CharacterStatsSO stats;
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Movement movementComponent;
        [NonSerialized] public Vector3 originalPos;
        [NonSerialized] public SplinesControl patrolComponent;
        //Implementing abstract classes
        private AIBaseState currentState;
        public AIChasingState chasingState = new AIChasingState();
        public AIReturnState returnState = new AIReturnState();
        public AIAttackState attackState = new AIAttackState();
        public AIPatrolState patrolState = new AIPatrolState();
        private void Awake()
        {
            if(stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }
            currentState = returnState;
            patrolComponent = GetComponent<SplinesControl>();
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementComponent = GetComponent<Movement>();
            originalPos = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }
        private void Update()
        {
            CheckDistanceBetweenPlayer();
            currentState.UpdateState(this);
        }

        private void CheckDistanceBetweenPlayer()
        {
            if (player == null) return;
            Vector3 enemyPos = transform.position;
            Vector3 playerPos = player.transform.position;
            distanceFromPlayer = Vector3.Distance(enemyPos, playerPos);
        }
        //Create a gizmo to designers can actually change this values
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangeRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);

        }

    }
}

