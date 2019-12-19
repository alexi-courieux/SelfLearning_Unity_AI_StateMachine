using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

namespace GameDevHQ.Extensions.Systems.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        public enum AIState
        {
            NonAlert,
            Alert,
            Engaged,
            Searching
        }


        [SerializeField]
        private GameObject _target;
        [SerializeField]
        private AIState _currentState;
        [SerializeField]
        private bool _standingGuard = true;
        [SerializeField]
        private List<GameObject> _wayPoints;
        private int _currentWaypoint = 0;
        [SerializeField]
        private float detectionRange;

        private NavMeshAgent _agent;
        private Renderer _renderer;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _renderer = GetComponent<Renderer>();
            if(_target == null)
            {
                try
                {
                    _target = GameObject.FindGameObjectWithTag("Player");
                }
                catch (UnityException ue)
                {
                    Debug.LogError("Target failed to be assigned");
                }
            }

        }

        private void Update()
        {
            switch (_currentState)
            {
                case AIState.NonAlert:
                    Debug.Log("Not Alert...");
                    _renderer.material.color = Color.green;
                    if(_standingGuard)
                    {
                        //check for enemy in radius
                        //if enemy in radius
                        //currentState = AIState.Alert
                        return;
                    }
                    else
                    {
                        //cycle through way points
                        if(_wayPoints.Count > 0)
                        {
                            _agent.SetDestination(_wayPoints[_currentWaypoint].transform.position);
                            float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
                            float distanceToWaypoint = Vector3.Distance(_wayPoints[_currentWaypoint].transform.position, transform.position);

                            if(distanceToTarget < detectionRange)
                            {
                                _currentState = AIState.Alert;
                            } else
                            {
                                Debug.Log(distanceToTarget);
                            }

                            if(distanceToWaypoint < 1.0f)
                            {
                                _currentWaypoint++;
                                if (_currentWaypoint >= _wayPoints.Count) _currentWaypoint = 0;
                            }
                        }
                    }
                    break;
                case AIState.Alert:
                    Debug.Log("Alert...");
                    _renderer.material.color = new Color(255,165,0);
                    //how much time has been spent in this state ?
                    //enough to go to engaged ?
                    //or go back to non-alert
                    _agent.SetDestination(_target.transform.position);
                    break;
                case AIState.Engaged:
                    Debug.Log("Engaged...");
                    _renderer.material.color = Color.red;
                    break;
                case AIState.Searching:
                    Debug.Log("Searching...");
                    _renderer.material.color = Color.yellow;
                    break;
            }
        }
    }
}