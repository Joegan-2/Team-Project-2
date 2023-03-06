using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    public bool IsStunned = false;
    public GameObject player;
    ProjectileController playerController;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<ProjectileController>();
    }

    private void Update()
    {
        if (IsStunned == false && playerController.isSafe == false)
        {
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        navMeshAgent.destination = movePositionTransform.position;
        } else {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;    
        }
    }
}
