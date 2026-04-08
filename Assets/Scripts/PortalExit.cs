using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalExit : MonoBehaviour
{
    [Header("Portal setup")]
    [SerializeField] private GameObject portalVisualRoot;
    [SerializeField] private Collider2D portalTriggerCollider;
    [SerializeField] private string nextSceneName;
    [SerializeField] private float enterDelay = 0.4f;
    [SerializeField] private bool startActive = false;

    [Header("Progression")]
    [SerializeField] private int levelNumber = 5;

    private bool portalActive = false;
    private bool used = false;

    private void Start()
    {
        SetPortalState(startActive);
    }

    public void ActivatePortal()
    {
        SetPortalState(true);
    }

    public void DeactivatePortal()
    {
        SetPortalState(false);
    }

    private void SetPortalState(bool active)
    {
        portalActive = active;

        if (portalVisualRoot != null)
            portalVisualRoot.SetActive(active);

        if (portalTriggerCollider != null)
            portalTriggerCollider.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!portalActive || used)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerSequenceActor actor = other.GetComponentInParent<PlayerSequenceActor>();

        if (actor == null)
        {
            Debug.LogError("Player entered portal, but no PlayerSequenceActor was found.");
            return;
        }

        StartCoroutine(EnterPortal(actor));
    }

    private IEnumerator EnterPortal(ISequenceControllable playerController)
    {
        used = true;

        playerController.LockControl();
        playerController.SetVisible(false);

        yield return new WaitForSeconds(enterDelay);

        if (ProgressionSystem.Instance != null)
        {
            ProgressionSystem.Instance.CompleteLevel(levelNumber);
            Debug.Log("After CompleteLevel, currentLevel = " + ProgressionSystem.Instance.currentLevel);
        }
        else
        {
            Debug.LogWarning("No ProgressionSystem found when completing level " + levelNumber);
        }

        SceneManager.LoadScene(nextSceneName);
    }
}