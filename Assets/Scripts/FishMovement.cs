﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] GameObject[] walls;

    [Header("SinMovement")]
    [SerializeField] private Vector3 movementVector = new Vector3(-20f, 0f, 0f);
    [SerializeField] float period = 6f;

    private Rigidbody2D rb2D;
    private float movementFactor;
    private bool isFacingRight = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
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

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject wall in walls)
        {
            if (collision.gameObject == wall)
            {
                Destroy(gameObject);
            }
        }
    }*/
    private void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
