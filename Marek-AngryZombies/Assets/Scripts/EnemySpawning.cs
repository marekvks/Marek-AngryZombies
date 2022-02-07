using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    /*
     * 
     *  Enemy Spawning ver.0 --> instantiating (object pooling isn't included)
     * 
     */

    private ScoreManager scoreManager;

    public GameObject zombie;

    public List<Transform> spawnAreas = new List<Transform>();
    private List<GameObject> zombiesSpawned = new List<GameObject>();

    float count;

    private bool isWaveOneActivated = false;
    private bool canSpawn = true;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        Debug.Log(scoreManager.score);

        foreach (GameObject enemy in zombiesSpawned)
        {
            if (count == zombiesSpawned.Count)
            {
                canSpawn = true;
            } else if (zombie)
            {
                canSpawn = false;
                count += 1;
            }
        }

        if (scoreManager.score == 0f && !isWaveOneActivated)
        {
            WaveOne();
        } else if (scoreManager.score != 0 && scoreManager.score % 4 == 0 && canSpawn)
        {
            UpgradeZombies();
        }
    }

    private void WaveOne()
    {
        isWaveOneActivated = true;
        Transform spawnArea;

        for (int i = 0; i < 4; i++)
        {
            spawnArea = spawnAreas[Random.Range(0, 3)];
            Instantiate(zombie, spawnArea);
        }
    }

    private void UpgradeZombies()
    {
        Transform spawnArea;
        float spawnAmmount = scoreManager.score + 4f;
        
        for (int i = 0; i < spawnAmmount; i++)
        {
            spawnArea = spawnAreas[Random.Range(0, 3)];
            GameObject spawnedZombie = Instantiate(zombie, spawnArea);
            spawnedZombie.GetComponent<EnemyHealth>().SetHealth(20f);
            spawnedZombie.GetComponent<EnemyCombat>().SetDamage(15f);
            spawnedZombie.GetComponent<EnemyMovement>().SetSpeed(2f);

            zombiesSpawned.Add(spawnedZombie);
        }
    }
}
