using UnityEngine;

public class EnterHospital : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); // make player disappear for now <-- this will get changed later
        }
    }
}