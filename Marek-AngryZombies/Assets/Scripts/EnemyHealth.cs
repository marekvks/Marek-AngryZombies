using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 100f;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 10;
    }

    private void Die()
    {
        // Play the animation
        gameObject.SetActive(false);
    }
}
