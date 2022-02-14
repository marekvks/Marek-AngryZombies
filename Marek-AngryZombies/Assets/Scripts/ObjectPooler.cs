using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject model;
        //  public float speed;
        public int count;
    }

    public float damage;
    public float health;

    //  Singleton Pattern
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    //  The end of the Singleton Pattern

    public List<GameObject> zombies = new List<GameObject>();
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDict;

    private void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.count; i++)
            {
                GameObject zombie = Instantiate(pool.model);
                zombie.SetActive(false);
                zombies.Add(zombie);
                objectPool.Enqueue(zombie);
            }

            poolDict.Add(pool.name, objectPool);
        }
    }

    public void SpawnFromPool(string name, Vector3 spawnArea)
    {
        GameObject zombieToSpawn = poolDict[name].Dequeue();


        Upgrade(zombieToSpawn);

        zombieToSpawn.transform.position = spawnArea;
        zombieToSpawn.SetActive(true);

        poolDict[name].Enqueue(zombieToSpawn);
    }

    public void Upgrade(GameObject zombie)
    {
        zombie.GetComponent<EnemyCombat>().AddDamage(damage);
        zombie.GetComponent<EnemyHealth>().AddHealth(health);
    } 
}