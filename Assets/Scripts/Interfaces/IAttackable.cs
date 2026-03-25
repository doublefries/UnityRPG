using UnityEngine;

public interface IAttackable
{
    // how much attack damage is dealt per hit
    int AttackDamage { get; }
    // range of hitbox that can actually be used to deal damage
    float AttackRange { get; }
    // intervals between attacks/hits 
    float AttackCooldown { get; }
    // enemy can attack player, object, or other enemy, may change this
    // attacker depends on a abstraction
    void Attack(IDamageable target);
}
