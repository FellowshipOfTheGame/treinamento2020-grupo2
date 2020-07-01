using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject treasure;

    [Header("Movement")]
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float midAirControl = 0.9f;
    [SerializeField] private float jumpVelocity = 3f;

    [Header("MeleeAttack")]
    [SerializeField] private int meleeAttackDamage = 10;
    [SerializeField] private float meleeAttackRate = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform meleeAttackPoint = null;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayerMask = default;

    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2D;
    private float nextMeleeAttackTime = 0f;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool isFacingRight = true;
    private bool canMove = true;
    private bool canOpenTreasure = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            Movement();
            Jump();
            
            if (Time.time >= nextMeleeAttackTime)
                if (Input.GetKeyDown(KeyCode.H))
                {
                    MeleeAttack();
                    nextMeleeAttackTime = Time.time + 1f / meleeAttackRate;
                }

            if (rb2D.velocity.x > 0f && !isFacingRight)
            {
                Flip();
            }
            else if (rb2D.velocity.x < 0f && isFacingRight)
            {
                Flip();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                canOpenTreasure = true;
            }
            else
            {
                canOpenTreasure = false;
            }
        }
    }

    private void Jump()
    {
        animator.SetFloat("Yspeed", rb2D.velocity.y);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpVelocity);
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }

        //Allows the user to change the height of the jump depending on how long they hold their space bar
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpVelocity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //When the player releases the Space bar, then stop the jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (Mathf.Abs(rb2D.velocity.y) < float.Epsilon)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void Movement()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));

        if (Input.GetKey(KeyCode.A))
        {
            if (IsGrounded())
            {
                rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -moveSpeed, moveSpeed), rb2D.velocity.y);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (IsGrounded())
                {
                    rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
                }
                else
                {
                    rb2D.velocity += new Vector2(moveSpeed * midAirControl * Time.deltaTime, 0);
                    rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -moveSpeed, moveSpeed), rb2D.velocity.y);
                }
            }
            else
            {
                if (IsGrounded())
                    rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);

        Debug.Log(raycastHit2D.collider);

        return raycastHit2D.collider != null;
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("MeleeAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Melee hit in " + enemy.name);

            enemy.GetComponent<Enemy>().TakeDamage(meleeAttackDamage);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void StopMoving()
    {
        canMove = false;
        rb2D.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (!meleeAttackPoint)
        {
            return;
        }

        Gizmos.DrawWireSphere(meleeAttackPoint.position, attackRange);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == treasure && canOpenTreasure)
        {
            treasure.GetComponent<Animator>().SetTrigger("Open");
            Debug.Log("YOU WON!!");
            StartCoroutine(LoadWinScene());
        }
    }
    IEnumerator LoadWinScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Win Scene");
    }

    public void AddForce(Vector2 force)
    {
        rb2D.AddForce(force);
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
