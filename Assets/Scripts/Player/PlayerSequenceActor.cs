using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSequenceActor : MonoBehaviour, ISequenceControllable
{
    [Header("Optional references")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Behaviour movementScript;
    [SerializeField] private GameObject visualRoot;
    [SerializeField] private Collider2D playerCollider;

    public void LockControl()
    {
        if (playerInput != null)
            playerInput.DeactivateInput();

        if (movementScript != null)
            movementScript.enabled = false;

        if (playerCollider != null)
            playerCollider.enabled = false;
    }

    public void UnlockControl()
    {
        if (playerCollider != null)
            playerCollider.enabled = true;

        if (movementScript != null)
            movementScript.enabled = true;

        if (playerInput != null)
            playerInput.ActivateInput();
    }

    public void SetVisible(bool visible)
    {
        if (visualRoot != null)
            visualRoot.SetActive(visible);
    }

    public void SetWorldPosition(Vector3 position)
    {
        transform.position = position;
    }
}