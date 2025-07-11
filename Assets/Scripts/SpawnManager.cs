using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Gives options for enemies to spawn (need to make this a list instead of one unit)
    public GameObject enemyPrefab;
    //public Transform[] spawnPoints;

    // Parameters of spawning
    public int enemiesPerWave = 5;
    public float timeBetweenSpawns = 1f;

    // Counts waves (probably should use text to indicate what wave we on. preference thing)
    private int currentWave = 0;

    // Detects when wave is finished
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

            // add to wave int
            currentWave++;

            // spawn a wave (should make this func more complex)
            yield return StartCoroutine(SpawnWave());

        }
    }

    IEnumerator SpawnWave()
    {
        // currently, only spawns a certain amount
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // randomized timing in between
            float nextSpawnTime = Random.Range(0, timeBetweenSpawns);
            SpawnEnemy();
            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    void SpawnEnemy()
    {
        // spawns enemy on random location on borders of the map
        int[] borders = {-50, 50};

        int randomValueX = borders[Random.Range(0, borders.Length)];
        int randomValueY = borders[Random.Range(0, borders.Length)];
        Vector3 randomPos = new Vector3(
            randomValueX,
            randomValueY,
            transform.position.z
        );

        // instantiate given location
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

        // Tell the enemy to notify GameManager when it dies
        //enemy.GetComponent<Enemy>().OnDeath = OnEnemyDeath;

        enemiesAlive++;
    }
}