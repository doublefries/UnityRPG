using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("No GameManager found");
            return;
        }
        
        Character selected =  GameManager.instance.currentCharacter;

        if (selected == null)
        {
            Debug.LogError("No Character selected");
            return;
        }
        
        Debug.Log("Spawning: " + selected.name);

        if (selected.prefab != null)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
            GameObject player  = Instantiate(selected.prefab, spawnPos, Quaternion.identity);
            
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = 10;
        }
    }
}

