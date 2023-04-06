using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AI;

namespace RPG.Character
{
    public class SplinesControl : MonoBehaviour
    {
        //Serialized Fields
        [SerializeField] private GameObject splineObject;
        [SerializeField] private float pauseDuration = 2f;
        [SerializeField] private float walkDuration = 3f;
        //Components
        private NavMeshAgent agentComponent;
        private SplineContainer splineContainer;
        //Non-serialized fields
        private float splinePosition = 0f;
        private float splineLength = 0f;
        private float lengthWalked = 0f;
        private float walkTime = 0f;
        private float pauseTime = 0f;
        private bool isWalking = true;
        private void Awake()
        {
            if (splineObject == null)
            {
                Debug.LogWarning($"{name} gameObject does not have a spline attached");
            }
            splineContainer = splineObject.GetComponent<SplineContainer>();
            agentComponent = GetComponent<NavMeshAgent>();
            splineLength = splineContainer.CalculateLength();
        }
        public Vector3 GetNextPosition()
        {
            return splineContainer.EvaluatePosition(splinePosition);
        }
        public void CalculateNextPosition()
        {
            //Pausing from patrolling
            walkTime += Time.deltaTime;
            if(walkTime > walkDuration)
            {
                isWalking = false;
            } 
            if(!isWalking)
            {
                pauseTime += Time.deltaTime;
                if (pauseTime < pauseDuration)
                {
                    return;
                }
                ResetTimers();
            }
            lengthWalked += Time.deltaTime * agentComponent.speed;
            if(lengthWalked > splineLength)
            {
                lengthWalked = 0f;
            }
            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0f;
            walkTime = 0f;
            isWalking = true;
        }
    }

}
