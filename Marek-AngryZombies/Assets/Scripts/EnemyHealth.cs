using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Objects/Components")]
    private Animator animator;
    EnemyMovement EnemyMovement;

    [Header("Variables")]
    [SerializeField]
    private float currentHealth = 100f;
    public bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 10;
    }

    private void Die()
    {
        EnemyMovement.nmAgent.speed = 0;
        animator.SetBool("dead", true);
    }

    private void Vanish()
    {
        gameObject.SetActive(false);
    }
}