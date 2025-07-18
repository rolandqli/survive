using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Gives options for enemies to spawn (need to make this a list instead of one unit)
    public GameObject[] enemies;
    //public Transform[] spawnPoints;

    // Parameters of spawning
    public float timeBetweenSpawns = 1f;
    public float waveDuration = 30f;
    private GameObject player;

    // Counts waves (probably should use text to indicate what wave we on. preference thing)
    private int currentWave = 0;

    // Detects when wave is finished
    private int enemiesAlive = 0;
    //private bool spawning = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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

            // add to wave int
            currentWave++;
            //Debug.Log("Wave " +  currentWave);
            // spawn a wave (should make this func more complex)
            yield return StartCoroutine(SpawnWave());
            timeBetweenSpawns -= 0.3f;

        }
    }

    IEnumerator SpawnWave()
    {
        // currently, only spawns a certain amount
        float i = 0;
        while (i < waveDuration)
        {
            // randomized timing in between
            float nextSpawnTime = Random.Range(0, timeBetweenSpawns);
            i += nextSpawnTime;
            //Debug.Log("i " + i);

            SpawnEnemy();
            yield return new WaitForSeconds(nextSpawnTime);
        }
        //Debug.Log("Finished Wave");
    }

    void SpawnEnemy()
    {
        // spawns enemy on random location offscreen
        Vector2 randomPos = (Vector2)player.transform.position + Random.insideUnitCircle.normalized * 100;


        // instantiate given location
        int index = Random.Range(0, currentWave);
        GameObject enemyPrefab = enemies[index];
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

        // Tell the enemy to notify GameManager when it dies
        //enemy.GetComponent<Enemy>().OnDeath = OnEnemyDeath;

        enemiesAlive++;
    }
}