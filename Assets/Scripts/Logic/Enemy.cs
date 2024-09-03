using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour //Ienemy
{
    private const int StartHealth = 100;
    private int currentHealth;
    public float AttackDistance => 50f;

    public int CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }

    public void Start()
    {
        currentHealth = StartHealth;
    }

    public void Die()
    {
        IsDead = true;
        gameObject.SetActive(false);
    }

    public void Move(Vector3 newPosition)
    {
        transform.DOMove(newPosition, 0f);
    }

    public void Attack()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}