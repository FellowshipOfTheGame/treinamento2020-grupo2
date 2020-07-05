using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] int damage = 30;

    [Header("SinMovement")]
    [SerializeField] private Vector3 movementVector = new Vector3(-20f, 0f, 0f);
    [SerializeField] float period = 6f;

    [Header("Player")]
    [SerializeField] GameObject player;

    private Rigidbody2D rb2D;
    private PlayerLife playerLife;
    private float movementFactor;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerLife.TakeDamage(damage);
        }
    }
}
