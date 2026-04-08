using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.instance == null || GameManager.instance.currentCharacter == null)
        {
            return;
        }

        Character selected = GameManager.instance.currentCharacter;

        SpriteRenderer sr = GetComponent<SpriteRenderer>(); //Swap sprite
        if (sr != null && selected.icon != null)
        {
            sr.sprite = selected.icon;
        }

        Animator animator = GetComponent<Animator>(); //Swap animator controller
        if (animator != null && selected.animatorController != null)
        {
            animator.runtimeAnimatorController = selected.animatorController;
        }
    }
}