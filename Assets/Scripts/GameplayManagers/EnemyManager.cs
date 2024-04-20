using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActivityType // types of way enemies spawn 
{
    Wave, /// <summary>
    /// active spawn of many enemies
    /// </summary>
    Cleaning, /// <summary>
    /// no spawn, but not all of enemies spawned on wave are dead
    /// </summary>
    Rest  ///no spawn
}

public class EnemyManager : MonoBehaviour
{
    private GameObject[] players;

    [SerializeField] int spawnCoefficient; /// <summary>
    /// estimated amount of enemies spawned per wave
    /// </summary>
    [SerializeField] float restSeconds; /// <summary>
    /// time of rest state after enemies are killed
    /// </summary>
    [SerializeField] float timeBetweenSpawn; /// <summary>
    /// time between enemies spawn during wave
    /// </summary>

    [SerializeField] List<GameObject> enemyPrefabs;
    private List<GameObject> spawnedEnemies;
    private GameObject[] spawns;

    private EnemyActivityType activityType;
    private int currentWaveNumber;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        spawns = GameObject.FindGameObjectsWithTag("Spawner");

        spawnedEnemies = new List<GameObject>();
    }

    private void Start()
    {
        currentWaveNumber = 1;
        StartWave(currentWaveNumber);
    }

    private void Update()
    {
        if (activityType == EnemyActivityType.Cleaning)
        {
            if (spawnedEnemies.Count < 1)
            {
                activityType = EnemyActivityType.Rest;
            }
        }
    }

    private void changeActivityType(EnemyActivityType activity)
    {
        activityType = activity;
        foreach (GameObject p in players)
        {
            //PlayerInterface pi = p.GetComponent<PlayerInterface>();
            //pi.DisplayWarningOnActivityType(activity);
        }
    }

    private void SummonRandomEnemyOnRandomSpawner()
    {
        SummonRandomEnemy(spawns[Random.Range(0, spawns.GetLength(0) - 1)].transform.position);
    }

    private void SummonRandomEnemy(Vector3 SpawnCoords)
    {
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        enemy = Instantiate(enemy, SpawnCoords, Quaternion.identity);
        spawnedEnemies.Add(enemy);
    }

    private void StartWave(int waveNumber)
    {
        changeActivityType(EnemyActivityType.Wave);
        int enemiesAmount = (int)(waveNumber * 1.5) * spawnCoefficient;
        StartCoroutine(ISpawnRandomEnemies(enemiesAmount));
    }

    private IEnumerator ISpawnRandomEnemies(int enemiesAmount)
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            SummonRandomEnemyOnRandomSpawner();
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        StartRest();
    }

    private void StartRest()
    {
        changeActivityType(EnemyActivityType.Rest);
        StartCoroutine(IStartRest());
    }

    private IEnumerator IStartRest()
    {
        yield return new WaitForSeconds(restSeconds);
        StartWave(currentWaveNumber + 1);
    }
}
