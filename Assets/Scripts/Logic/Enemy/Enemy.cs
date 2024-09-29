using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IEnemy
{
    private const float StartHealth = 100;

    private readonly TimeSpan _attackDelay = TimeSpan.FromSeconds(0.5f);

    private float currentHealth;
    private float moveSpeed;
    private float damage=10f;   

    private bool isCanAttack;

    public float AttackDistance => 2.5f;
    public float CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }
    public float MoveSpeed => moveSpeed;
    public float Armor { get; }
    public float DamageResistance { get; }
    public float DamageReflection { get; }
    public float Vampirism { get; }


    public Transform Transform => transform;

    private void OnEnable()
    {
        currentHealth = StartHealth;
        IsDead = false;
        isCanAttack = true;
        moveSpeed = Random.Range(1, 3f);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        IsDead = true;
    }

    //to newPosition
    public void Move(Vector3 offset)
    {
        transform.position += offset;
    }

    public virtual void Attack(IPlayer player, IDamageSystem damageSystem)
    {
        if (!isCanAttack)
        {
            return;
        }

        isCanAttack = false;

        AttackPrepare(player, damageSystem).Forget();
    }

    private async UniTaskVoid AttackPrepare(IPlayer player, IDamageSystem damageSystem)
    {
        await UniTask.Delay(_attackDelay);

        AttackProcess(player, damageSystem);
    }

    private void AttackProcess(IPlayer player, IDamageSystem damageSystem)
    {
        //to damage system
        damageSystem.TakeDamage(this, player, damage);

        PostAttack().Forget();
    }

    private async UniTask PostAttack()
    {
        await UniTask.Delay(_attackDelay);

        isCanAttack = true;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

    public void TakeDamage(IDamageble attacker, float damage)
    {
        currentHealth -= damage;
    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth= Math.Clamp(currentHealth, 0, StartHealth);
    }
}

