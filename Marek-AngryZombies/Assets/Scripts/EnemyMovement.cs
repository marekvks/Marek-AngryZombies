using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Objects/Components")]
    public Transform player;
    private NavMeshAgent nmAgent;

    private void Start()
    {
        nmAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (nmAgent != null)
        {
            nmAgent.SetDestination(player.position);
        }
    }
}