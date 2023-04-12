using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField]
    private AsteroidLibrary library;

    [SerializeField, Tooltip("Spawn rate per minute")]
    private float spawnRate = 10;

    [SerializeField, Tooltip("As percentage")]
    private float speedUpRate = 1.01f;

    [SerializeField]
    private Vector2 spawnRange = new Vector2(5, 5);

    [SerializeField]
    private Transform spawnPoint;

    private Coroutine generatorCoroutine;

    public void StartGenerating()
    {
        generatorCoroutine =  StartCoroutine(GeneratingLoop());
    }

    public void StopGenerator()
    {
        StopCoroutine(generatorCoroutine);
        generatorCoroutine = null;
    }

    private IEnumerator GeneratingLoop()
    {
        var currentSpawnRate = spawnRate;

        // this loop can be when we're stopping coroutine
        while (true)
        {
            var randomLevel = Random.Range(1, 4);
            var asteroidDef = library.GetAsteroid(randomLevel);
            var newAsteroid = Instantiate(asteroidDef, GetRandomSpawn(), Quaternion.identity, transform);

            newAsteroid.transform.position = GetRandomSpawn();

            newAsteroid.Initialize();
            newAsteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);

            yield return new WaitForSeconds(currentSpawnRate);

            currentSpawnRate *= speedUpRate;
        }
    }

    private void OnAsteroidDestroyed(AsteroidInfo asteroid)
    {
        // when medium or big size asteroid is destroyed, we're spawning 2 lower levels asteroids.
        if (asteroid.Level > 1)
        {
            var nextLevel = asteroid.Level - 1;
            var asteroidDef = library.GetAsteroid(nextLevel);

            var spawnPoints = GetSpawnPoints(asteroid);
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var newAsteroid = Instantiate(asteroidDef, spawnPoints[i], Quaternion.identity, transform);
                newAsteroid.Initialize();
                newAsteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);
            }
        }
    }
    
    /// <summary>
    /// Return random value in the range of player
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomSpawn()
    {
        return spawnPoint.position + new Vector3(Random.Range(-spawnRange.x, spawnRange.x), 0, Random.Range(-spawnRange.y, spawnRange.y));
    }

    /// <summary>
    /// Returns 2 positions with slight offset.
    /// </summary>
    /// <param name="asteroid"></param>
    /// <returns></returns>
    private Vector3[] GetSpawnPoints(AsteroidInfo asteroid)
    {
        return new Vector3[2] 
        {
            asteroid.transform.position + asteroid.transform.rotation * Vector3.left * 0.5f,
            asteroid.transform.position + asteroid.transform.rotation * Vector3.right * 0.5f
        };
    }
}
