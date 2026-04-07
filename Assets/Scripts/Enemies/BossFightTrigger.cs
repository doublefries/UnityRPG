using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private BossEnemy boss;
    [SerializeField] private BossHealthBarUI bossHealthBarUI;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered boss trigger: " + other.name);

        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player started boss fight");
        triggered = true;

        if (boss != null)
        {
            boss.StartFight();
        }
        else
        {
            Debug.LogWarning("Boss reference is missing.");
        }

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.ShowBar();
        }
}
}