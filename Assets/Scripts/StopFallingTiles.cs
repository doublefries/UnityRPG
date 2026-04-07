using UnityEngine;

public class StopFallingTiles : MonoBehaviour
{
    [SerializeField] private FallingTiles fallingTiles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fallingTiles.StopFalling();
        }
    }
}