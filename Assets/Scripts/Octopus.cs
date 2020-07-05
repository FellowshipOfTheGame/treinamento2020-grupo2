using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
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
}
