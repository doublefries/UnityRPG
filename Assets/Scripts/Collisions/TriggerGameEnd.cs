using System.Collections;
using UnityEngine;

public class TriggerGameEnd : MonoBehaviour
{
    [Header("Sequence")]
    [SerializeField] private Transform exitPoint;
    [SerializeField] private GameObject temporaryPotionVisual;
    [SerializeField] private float hiddenTime = 2f;
    [SerializeField] private float potionShowTime = 0.75f;

    [SerializeField] private PortalExit portalToActivate;
    
    private bool used = false;

    private void Start()
    {
        if (temporaryPotionVisual != null)
            temporaryPotionVisual.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerSequenceActor actor = other.GetComponentInParent<PlayerSequenceActor>();

        if (actor == null)
        {
            Debug.LogError("Player entered trigger, but no PlayerSequenceActor was found on the player.");
            return;
        }

        StartCoroutine(RunSequence(actor));
    }

    private IEnumerator RunSequence(ISequenceControllable playerController)
    {
        used = true;

        playerController.LockControl();
        playerController.SetVisible(false);

        yield return new WaitForSeconds(hiddenTime);

        if (exitPoint != null)
            playerController.SetWorldPosition(exitPoint.position);

        playerController.SetVisible(true);

        if (temporaryPotionVisual != null)
            temporaryPotionVisual.SetActive(true);
        
        if (portalToActivate != null)
            portalToActivate.ActivatePortal();

        playerController.UnlockControl();

        yield return new WaitForSeconds(potionShowTime);

        if (temporaryPotionVisual != null)
            temporaryPotionVisual.SetActive(false);
    }
}