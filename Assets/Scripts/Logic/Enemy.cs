using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    private int startHealth = 100;
    private int currentHealth;  

    public Action<Enemy> OnDead;

    public void Start()
    {
        currentHealth = startHealth;
    }

    public void Die()
    {
       OnDead?.Invoke(this);
       gameObject.SetActive(false);
    }  

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    } 
    
}