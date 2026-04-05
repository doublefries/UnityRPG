using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private VinedWall linkedWall;
    [SerializeField] private Sprite pressedSprite; //For visual change after the pressure plate is used
    
    private bool _activated = false;
    private SpriteRenderer _sr;
    
    void Start()
    {
        _sr =  GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated) //If already activated do nothing
        {
            return;
        }

        _activated = true;

        if (_sr != null && pressedSprite != null) //Visually change the sprite once its pressed
        {
            _sr.sprite = pressedSprite;
        }

        if (linkedWall != null)
        {
            linkedWall.RemoveWall();
        }
        else
        {
            Debug.Log("No VinedWall linked to this pressure plate.");
        }
    }
}
