using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Objects/Components")]
    private PlayerMovement playerMovement;
    private Animator animator;

    [Header("Variables")]
    [SerializeField]
    private float health = 100f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (playerMovement != null && animator != null)
            {
                playerMovement.canMove = false;
                animator.SetTrigger("dead");
            }
        }
    }
}