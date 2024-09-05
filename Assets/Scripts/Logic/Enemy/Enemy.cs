using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour //Ienemy
{
    private const float StartHealth = 100;
    private float currentHealth;
    public float AttackDistance => 2.5f;
    public float MoveSpeed => moveSpeed;

    private float moveSpeed;

    public float CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }
    private bool isCanAttack;
    private float attackDelay = 1f;  

    public void Start()
    {
        currentHealth = StartHealth;
        moveSpeed = Random.Range(1, 3f);        
        IsDead = false;
        isCanAttack = true;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        IsDead = true;       
    }

    public void Move(Vector3 newPosition)
    {
        transform.position+=newPosition;
    }

    public void Attack(IPlayer player)
    {
       if(!isCanAttack) return;

       player.TakeDamage(1f);
       AttackDelayTimer().Forget();
    }  

    private async UniTaskVoid AttackDelayTimer()
    {
        isCanAttack = false;
        await UniTask.Delay((int)attackDelay * 1000);
        isCanAttack = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        currentHealth = StartHealth;
        IsDead = false;
    }
}