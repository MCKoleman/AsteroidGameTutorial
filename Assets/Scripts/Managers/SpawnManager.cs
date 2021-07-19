using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // Inspector fields
    public GameObject spawnHolder;
    [SerializeField]
    private GameObject[] asteroids;
    [SerializeField]
    private float asteroidSpawnX;
    [SerializeField]
    private float baseAsteroidSpawnRate;

    // Progression variables
    private const float ASTEROID_SPAWN_MOD = 5.0f;
    private const int LEVEL_ASTEROID_NUM_MOD = 5;
    [SerializeField]
    private float asteroidSpawnTimer;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int asteroidsSpawnedThisLevel;
    
    // Initializes the singleton. Should only be called from GameManager.
    public void Init()
    {
        currentLevel = 0;
        asteroidSpawnTimer = 0.0f;
        asteroidsSpawnedThisLevel = 0;
    }

    private void Update()
    {
        // Only spawn asteroids when the game is active
        if(GameManager.Instance.IsGameActive)
        {
            // Check what the maximum for the timer is at this level
            float spawnTimerMax = ASTEROID_SPAWN_MOD / (baseAsteroidSpawnRate + currentLevel);

            // If the timer hasn't completed yet, keep it going
            if(asteroidSpawnTimer < spawnTimerMax)
            {
                asteroidSpawnTimer += Time.deltaTime;
            }
            // If the timer has completed, reset it and spawn an asteroid
            else
            {
                SpawnRandomAsteroid();
                asteroidSpawnTimer = 0.0f;
            }
        }
    }

    // Spawns a random asteroid
    private void SpawnRandomAsteroid()
    {
        // Get a random asteroid from the asteroids array
        int randomIndex = Random.Range(0, asteroids.Length);

        // Set the spawn position of the new asteroid
        Vector3 spawnPosition = new Vector3(asteroidSpawnX, 0.0f, 0.0f);

        // Spawn a new asteroid at the given location and sets the spawnHolder as its parent
        GameObject newAsteroid = Instantiate(asteroids[randomIndex], spawnPosition, Quaternion.identity, spawnHolder.transform);

        // Initialize the asteroid based on the level
        newAsteroid.GetComponent<Asteroid>().Init(currentLevel);

        // Count how many asteroids have spawned this level
        asteroidsSpawnedThisLevel++;

        // Give player a point for each spawned asteroid
        ScoreManager.Instance.AddScore(1);
        CheckLevelProgression();
    }

    // Checks the level progression
    private void CheckLevelProgression()
    {
        // Find out how many asteroids are supposed to spawn this level
        int levelMaxAsteroids = currentLevel * LEVEL_ASTEROID_NUM_MOD;

        // If enough asteroids have spawned, increment the level
        if(asteroidsSpawnedThisLevel >= levelMaxAsteroids)
        {
            currentLevel++;
            asteroidsSpawnedThisLevel = 0;
        }
    }
}
