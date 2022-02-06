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

    private bool isWaveOneActivated = false;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (scoreManager.score == 0f && !isWaveOneActivated)
        {
            WaveOne();
        }
    }

    private void WaveOne()
    {
        isWaveOneActivated = true;
        Random rnd = new Random();
        Transform spawnArea;

        for (int i = 0; i < 8; i++)
        {
            spawnArea = spawnAreas[Random.Range(0, 3)];
            Instantiate(zombie, spawnArea);
        }
    }
}
