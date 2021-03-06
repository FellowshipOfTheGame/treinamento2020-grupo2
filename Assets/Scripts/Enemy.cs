﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private int health = 100;
    [SerializeField] private HealthBar healthBar;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
