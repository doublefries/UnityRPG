using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    // PlayerSpawner.cs
    private void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager is NULL");
            return;
        }

        if (GameManager.instance.currentCharacter == null)
        {
            Debug.LogError("currentCharacter is NULL");
            return;
        }

        Debug.Log("Current character name: " + GameManager.instance.currentCharacter.name);
        Debug.Log("Current character prefab: " + GameManager.instance.currentCharacter.prefab);

        GameObject prefab = GameManager.instance.currentCharacter.prefab;
        if (prefab != null)
        {
            Vector3 spawnPos = spawnPoint != null
                ? new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f)
                : new Vector3(transform.position.x, transform.position.y, 0f);

            GameObject player = Instantiate(prefab, spawnPos, Quaternion.identity);
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = 1;
        }
        else
        {
            Debug.LogError("Prefab is NULL on currentCharacter: " + GameManager.instance.currentCharacter.name);
        }
    }
}
