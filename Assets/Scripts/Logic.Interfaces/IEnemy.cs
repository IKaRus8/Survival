using UnityEngine;


public interface IEnemy : IDamageble
{
    public Transform Transform { get; }
    public float AttackDistance { get; }
    public float MoveSpeed { get; }
    public bool IsDead { get; }

    public float CurrentHealth { get; }

    void Die();
    void Reset();
    void Move(Vector3 offset);
    void Attack(IPlayer player, IDamageSystem damageSystem);
}

