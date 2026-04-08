using UnityEngine;

public class Level1PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [Header("Level 1 overrides")]
    [SerializeField] private bool allowVerticalMovementInLevel1 = false;
    [SerializeField] private bool enableJumpsInLevel1 = true;

    private void Start()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementOptions(
                allowVerticalMovementInLevel1,
                enableJumpsInLevel1
            );
        }
    }
}