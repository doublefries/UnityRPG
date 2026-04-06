using UnityEngine;
    public class LavaHazard : MonoBehaviour
    {
        [SerializeField] private Level1UIManager levelManager;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                levelManager.PlayerDied();
            }
        }
    }