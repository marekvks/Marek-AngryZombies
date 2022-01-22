using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float zombieHealth = 100f;

    private void Update()
    {
        if (zombieHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
