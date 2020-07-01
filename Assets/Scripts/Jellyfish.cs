using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] int health = 100;
    [SerializeField] private HealthBar healthBar;

    [Header("Damage")]
    [SerializeField] int damage = 50;

    [Header("Player")]
    [SerializeField] GameObject player;

    private Animator animator;
    private PlayerLife playerLife;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerLife.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage, ref List<Jellyfish> list)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death(ref list);
        }
    }
    private void Death(ref List<Jellyfish> list)
    {
        list.Remove(this);
        Destroy(gameObject);
    }
}
