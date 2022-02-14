﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Objects/Components")]
    private Collider col;
    private Animator animator;

    [Header("Spawn Pickups")]
    public List<GameObject> prefabs = new List<GameObject>();

    [Header("Scripts")]
    EnemyMovement EnemyMovement;
    ScoreManager scoreManager;

    [Header("Variables")]
    [SerializeField]
    private float currentHealth = 100f;
    public bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnemyMovement = GetComponent<EnemyMovement>();
        col = GetComponent<Collider>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        scoreManager.AddScore(1f);
        col.enabled = false;
        EnemyMovement.nmAgent.speed = 0;
        RandomSpawnAmmo();
        animator.SetBool("dead", true);
    }

    private void Vanish()
    {
        gameObject.SetActive(false);
    }

    private void RandomSpawnAmmo()
    {
        int spawnRate = Random.Range(3, 11);    //  30% chance to spawn an ammunition
        Debug.Log(spawnRate);

        if (spawnRate <= 3)
        {
            int randomWeapon = Random.Range(0, 3);
            GameObject weaponToSpawn = prefabs[randomWeapon];
            Instantiate(weaponToSpawn, transform.position, Quaternion.identity);
        }
    }

    public void AddHealth (float ammount)
    {
        currentHealth += ammount;
    }
}