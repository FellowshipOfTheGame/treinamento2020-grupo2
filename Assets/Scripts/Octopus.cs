using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] int health = 100;
    [SerializeField] private HealthBar healthBar;

    [Header("Damage")]
    [SerializeField] int damage = 30;

    [Header("Player")]
    [SerializeField] GameObject player;

    private Animator animator;
    private PlayerLife playerLife;

    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 10f)
        {
            animator.SetTrigger("IsInRange");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerLife.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage, ref List<Octopus> list)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death(ref list);
        }
    }
    private void Death(ref List<Octopus> list)
    {
        list.Remove(this);
        Destroy(gameObject);
    }
}
