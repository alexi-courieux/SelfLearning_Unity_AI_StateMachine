using UnityEngine;
using System.Collections;
using UnityEngine.AI;

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

        private NavMeshAgent _agent;
        private Renderer _renderer;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _renderer = GetComponent<Renderer>();

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
                        _agent.SetDestination(_target.transform.position);
                    }
                    break;
                case AIState.Alert:
                    
                    
                    Debug.Log("Alert...");
                    _renderer.material.color = new Color(255,165,0);
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