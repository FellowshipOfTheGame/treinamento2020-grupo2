using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float midAirControl = 0.9f;
    [SerializeField] private float jumpVelocity = 3f;
    [SerializeField] private LayerMask groundLayerMask = default;

    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool isFacingRight = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        Jump();

        if (Input.GetKeyDown(KeyCode.H))
        {
            MeleeAttack();
        }

        if (rb2D.velocity.x > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (rb2D.velocity.x < 0f && isFacingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
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
        animator.SetBool("MeleeAttack", true);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
