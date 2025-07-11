using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int enemiesPerWave = 5;
    public float timeBetweenSpawns = 1f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    //private bool spawning = false;

    void Start()
    {
        StartCoroutine(WaveLoop());

    }

    public void enemyDeath()
    {
        enemiesAlive--;
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            // Wait until all enemies are gone before starting next wave
            yield return new WaitUntil(() => enemiesAlive == 0);

            currentWave++;
            Debug.Log("Wave " + currentWave + " starting");

            yield return StartCoroutine(SpawnWave());

            // Wait for enemies to be destroyed
            Debug.Log("Waiting for enemies to be defeated...");
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            float nextSpawnTime = Random.Range(0, timeBetweenSpawns);
            SpawnEnemy();
            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    void SpawnEnemy()
    {
        int[] borders = {-50, 50};

        int randomValueX = borders[Random.Range(0, borders.Length)];
        int randomValueY = borders[Random.Range(0, borders.Length)];
        Vector3 randomPos = new Vector3(
            randomValueX,
            randomValueY,
            transform.position.z
        );
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

        // Tell the enemy to notify GameManager when it dies
        //enemy.GetComponent<Enemy>().OnDeath = OnEnemyDeath;

        enemiesAlive++;
    }
}