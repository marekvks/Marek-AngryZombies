using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ScoreManager scoreManager;
    private ObjectPooler objectPooler;

    public List<Transform> spawnAreas = new List<Transform>();

    private bool canSpawn = true;
    int spawnCount = 4;

    private int notActiveCount;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void Update()
    {
        foreach (GameObject zombie in objectPooler.zombies)
        {
            if (zombie.activeInHierarchy)
            {
                canSpawn = false;
                notActiveCount = 0;
            }
            else if (!zombie.activeInHierarchy && notActiveCount < 60)
            {
                notActiveCount += 1;
            }
        }

        if (notActiveCount == 60)
        {
            canSpawn = true;
        }

        if (canSpawn)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Transform spawnPos = spawnAreas[Random.Range(0, 4)];
                ObjectPooler.Instance.SpawnFromPool("zombie", spawnPos.position);
            }
            spawnCount += 2;
        }
    }
}
