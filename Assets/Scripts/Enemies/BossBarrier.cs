using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    [SerializeField] private BossEnemy boss;
    [SerializeField] private GameObject barrier;

    private void Start()
    {
        if (boss == null)
        {
            Debug.LogWarning("BossBarrier: Boss reference is missing.");
            return;
        }

        if (barrier == null)
        {
            Debug.LogWarning("BossBarrier: Barrier reference is missing.");
            return;
        }

        boss.OnDeath += OpenBarrier;
    }

    private void OnDestroy()
    {
        if (boss != null)
            boss.OnDeath -= OpenBarrier;
    }

    private void OpenBarrier()
    {
        barrier.SetActive(false);
    }
}