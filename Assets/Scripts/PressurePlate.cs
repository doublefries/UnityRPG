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
        
    }
}
