using UnityEngine;

// Game Attribute: adds an option to Unity's right-click menu to create new stats assest easily 
[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Game/Enemy Stats")]

// inherets from ScriptableObject (just holds data)
public class EnemyStatsSO : ScriptableObject
{
    // these are all public because they just hold displayable data not the actual values
    // display name that shows up in unity inspector
    public string enemyName = "Enemy";
    // hits until enemy dies
    public int maxHealth = 3;
    // damage enemy deals per attack
    public int attackDamage = 1;
    // how fast the element moves in unity units/sec
    public float moveSpeed = 2f;
    // how close player must be before enemy can attack
    public float attackRange = 1.5f;
    // time between attacks
    public float attackCooldown = 1f;
    // how far away enemys recognize the player, beyond this they don't interact
    public float detectionRange = 8f;
}