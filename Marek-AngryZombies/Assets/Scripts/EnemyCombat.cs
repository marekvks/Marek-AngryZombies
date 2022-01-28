using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform player;
    public LayerMask target;
    public Transform center;
    private Animator animator;
    bool canHit = false;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;

    [SerializeField]
    private float damage = 25f;

    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(center.position, player.position - transform.position, Color.yellow);

        if (Physics.Raycast(center.position, player.position - transform.position, out hit, 2f, target) && enemyHealth.isDead != true)
        {
            playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
            canHit = true;
        }
        else
        {
            canHit = false;
        }

        if (animator != null)
        {
            animator.SetBool("hit", canHit);
        }
    }

    private void DealDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
