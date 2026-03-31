using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (GameManager.instance == null || GameManager.instance.currentCharacter == null) //Game manager is null as well as selected character
        {
            Debug.LogWarning("No character selected");
            return;
        } 
        
        GameObject prefab = GameManager.instance.currentCharacter.prefab;
        if (prefab != null)
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
        
    }
}
