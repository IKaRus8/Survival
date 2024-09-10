using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour //Ienemy
{
    private const float StartHealth = 100;
    
    private readonly TimeSpan _attackDelay = TimeSpan.FromSeconds(0.5f);

    private float currentHealth;
    private float moveSpeed;
    private bool isCanAttack;

    public float AttackDistance => 2.5f;
    public float CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }
    public float MoveSpeed => moveSpeed;

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

    public virtual void Attack(IPlayer player)
    {
        if (!isCanAttack)
        {
            return;
        }

        isCanAttack = false;
        
       AttackPrepare(player).Forget();
    }

    private async UniTaskVoid AttackPrepare(IPlayer player)
    {
        await UniTask.Delay(_attackDelay);
        
        AttackProcess(player);
    }

    private void AttackProcess(IPlayer player)
    {
        //to damage system
        player.TakeDamage(1f);

        PostAttack().Forget();
    }

    private async UniTask PostAttack()
    {
        await UniTask.Delay(_attackDelay);
        
        isCanAttack = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}