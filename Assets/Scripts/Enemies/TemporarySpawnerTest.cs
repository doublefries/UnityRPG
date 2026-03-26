using UnityEngine;

// Throwaway script that triggers a wave on scene start for testing
// Attach to the same GameObject as EnemySpawner, drag the spawner reference in
// Delete this once real level managers exist
public class SpawnerTest : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;

    private void Start()
    {
        // Spawn enemies immediately when the scene starts
        spawner.SpawnWave();
        // Log when all enemies are dead to prove the full loop works
        spawner.OnAllEnemiesDefeated += () => Debug.Log("All enemies defeated!");
    }
}