using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Objects/Components")]
    public Transform player;
    public NavMeshAgent nmAgent;
    private Animator animator;

    private void Start()
    {
        nmAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (nmAgent != null)
        {
            nmAgent.SetDestination(player.position);
        }

        if (animator != null)
        {
            animator.SetFloat("speed", nmAgent.speed);
        }
    }
}