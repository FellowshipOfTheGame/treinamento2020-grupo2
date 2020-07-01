﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] int health = 100;
    [SerializeField] private HealthBar healthBar;

    [Header("Attack")]
    [SerializeField] private int damage = 30;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Vector2 pushForce = new Vector2(10f, 0f);
    [SerializeField] private Transform AttackPoint = null;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject player;

    [Header("SinMovement")]
    [SerializeField] private Vector3 movementVector = new Vector3(-20f, 0f, 0f);
    [SerializeField] float period = 6f;

    [Header("Chase Movement")]
    [SerializeField] float speed = 5f;

    private Player playerScript;
    private PlayerLife playerLifeScript;
    private Rigidbody2D rb2D;
    private Vector2 movement;
    private bool canMove = true;
    private bool isFacingRight = true;
    private bool isFacingUp = true;
    private bool playerInRange = true;
    private float movementFactor;

    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        playerLifeScript = FindObjectOfType<PlayerLife>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            //ChaseMovement();          
            SinMovement();

            if (rb2D.velocity.x > 0f && !isFacingRight)
            {
                FlipHorizontal();
            }
            else if (rb2D.velocity.x < 0f && isFacingRight)
            {
                FlipHorizontal();
            }
        }
    }

    private void FixedUpdate()
    {
        //Move(movement);
    }

    private void ChaseMovement()
    {
        Vector3 direction = player.transform.position - transform.position;

        if (direction.x < 0f && direction.y < 0f && !isFacingUp)
        {
            FlipVertical();
        }
        else if (direction.x > 0f && direction.y > 0f && isFacingUp)
        {
            FlipVertical();
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb2D.rotation = angle;
        direction.Normalize();
        movement = direction;
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    private void Move(Vector2 direction)
    {
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    private void SinMovement()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 3f;
        Vector3 offset = movementFactor * movementVector;
        rb2D.velocity = offset;
    }

    private void Attack()
    {
        //Collider2D player = Physics2D.OverlapCircle(AttackPoint.position, attackRange, playerLayer);
        player.GetComponent<Player>().AddForce(pushForce);
        player.GetComponent<PlayerLife>().TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        if (!AttackPoint)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    private void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    private void FlipVertical()
    {
        isFacingUp = !isFacingUp;

        transform.Rotate(180f, 0f, 0f);
    }

    private void IsPlayerInRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 20, playerLayer);

        if (player)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
            Attack();
    }

    public void TakeDamage(int damage, ref List<Enemy> list)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death(ref list);
        }
    }
    private void Death(ref List<Enemy> list)
    {
        canMove = false;
        list.Remove(this);
        Destroy(gameObject);
    }
}
