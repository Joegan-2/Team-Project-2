using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    public GameObject player;
    ProjectileController playerScript;

    public bool IsStunned = false;
<<<<<<< Updated upstream
    public GameObject player;
    ProjectileController playerController;
=======
    public bool isSafe = false;
>>>>>>> Stashed changes

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
<<<<<<< Updated upstream
        playerController = player.GetComponent<ProjectileController>();
=======
        playerScript = player.GetComponent<ProjectileController>();
>>>>>>> Stashed changes
    }

    private void Update()
    {
<<<<<<< Updated upstream
        if (IsStunned == false && playerController.isSafe == false)
=======
        isSafe = playerScript.isSafe;
        if (IsStunned == false && isSafe == false)
>>>>>>> Stashed changes
        {
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        navMeshAgent.destination = movePositionTransform.position;
        } else {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;    
        }
    }
}
