using UnityEngine;

// Attach to a floor GameObject with a Collider2D set to "Is Trigger"
// Detects when the player enters/exits ice and tells their IcePhysicsController
public class IceZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to the player, ignore enemies and everything else
        if (!other.CompareTag("Player")) return;

        // Find IcePhysicsController on the player or its parents
        IcePhysicsController ice = other.GetComponent<IcePhysicsController>();
        if (ice == null)
            ice = other.GetComponentInParent<IcePhysicsController>();

        // Tell the controller: you're on ice now
        if (ice != null)
            ice.EnterIce();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Only react to the player
        if (!other.CompareTag("Player")) return;

        // Find IcePhysicsController on the player or its parents
        IcePhysicsController ice = other.GetComponent<IcePhysicsController>();
        if (ice == null)
            ice = other.GetComponentInParent<IcePhysicsController>();

        // Tell the controller: you're off ice now
        if (ice != null)
            ice.ExitIce();
    }
}