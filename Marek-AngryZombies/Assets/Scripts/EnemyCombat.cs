using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Objects/Components")]
    public Transform player;
    public Transform center;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private Animator animator;
    public LayerMask target;

    [Header("Audio")]
    private SoundManager soundManager;
    public AudioClip zombieHit;

    [SerializeField]
    private float damage = 25f;
    bool canHit = false;

    RaycastHit hit;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = gameObject.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        Debug.DrawRay(center.position, player.position - transform.position, Color.yellow);

        if (Physics.Raycast(center.position, player.position - transform.position, out hit, 2f, target) && enemyHealth.isDead != true)
        {
            playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
            canHit = true;
            transform.rotation = Quaternion.LookRotation(-(transform.position - player.position));
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
            soundManager.ZombieSound(zombieHit);
            playerHealth.TakeDamage(damage);
        }
    }

    public void SetDamage(float ammount)
    {
        damage += ammount;
    }

}
