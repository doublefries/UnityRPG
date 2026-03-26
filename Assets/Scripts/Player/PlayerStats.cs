using UnityEngine;

public class PlayerStats: MonoBehaviour
{
    [SerializeField] private int coins = 100;
    [SerializeField] private int strength = 10;
    [SerializeField] private float speed = 5f;

    private Weapon equippedWeapon;

    public int Coins => coins;
    public int Strength => strength;
    public float Speed => speed;
    public Weapon EquippedWeapon => equippedWeapon;

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount)
            return false;

        coins -= amount;
        return true;
    }

    public void IncreaseStrength(int amount)
    {
        strength += amount;
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
    }
}
