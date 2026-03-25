using System.Collections.Generic;
using UnityEngine;

// reusable component that spawns enemies and tracks when they all die 
public class EnemySpawner : MonoBehaviour
{

    // list of enemy prefabs to randomly pick from when spawning
    [SerializeField] private List<GameObject> enemyPrefabs;

    // positions where enemies can appear: empty GameObjects placed in the scene
    [SerializeField] private Transform[] spawnPoints;

    // maximum enemies per spawn/wave
    [SerializeField] private int maxEnemies = 5;

    // tracks all currently living enemies spawned by this spawner: enemies are added on spawn, removed on death
    private List<GameObject> activeEnemies = new List<GameObject>();

    // fires when the last enemy in a wave dies
    // progression trigger for level managers (spawn portal)
    public event System.Action OnAllEnemiesDefeated;

    // called by whatever controls the level flow: level manager, trigger zone, timer
    // public so external scripts can trigger spawning when needed
    public void SpawnWave()
    {
        // spawn the lesser of maxEnemies or available spawn points
        // prevents index out of range if maxEnemies is 5 but you only placed 3 spawn points
        int count = Mathf.Min(maxEnemies, spawnPoints.Length);

        for (int i = 0; i < count; i++)
        {
            // i might change how this works and make it level dependant rather than random
        
            // pick a random enemy prefab using a random index from 0 to list size - 1
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // clone that prefab into the scene at the spawn point's position with default rotation
            GameObject enemy = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);

            // add to tracking list so we know this enemy is alive
            activeEnemies.Add(enemy);

            // get the IDamageable interface from the spawned enemy
            // every enemy inherits from EnemyBase which implements IDamageable
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // local copy so each lambda captures its own reference, not the shared loop variable
                GameObject captured = enemy;

                // Subscribe to this enemy's death event: EnemyBase.Die() fires OnDeath triggers our OnEnemyDied
                damageable.OnDeath += () => OnEnemyDied(captured);
            }
        }
    }

    // called automatically when any tracked enemy dies by the OnDeath event we subbed in SpawnWave
    private void OnEnemyDied(GameObject enemy)
    {
        // remove the dead enemy from tracking list
        activeEnemies.Remove(enemy);

        // if the list is empty every enemy in the wave is dead
        // fire the event so the level manager knows to progress
        if (activeEnemies.Count == 0)
            OnAllEnemiesDefeated?.Invoke();
    }
}