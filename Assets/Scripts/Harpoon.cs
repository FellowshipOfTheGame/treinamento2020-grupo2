using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 10;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject, 2f);
    }
}
