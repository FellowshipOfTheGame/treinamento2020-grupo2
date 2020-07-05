using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 20;

    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rb2D;

    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(polygonCollider2D.bounds.center, polygonCollider2D.bounds.size, 0f, Vector2.down, 0.1f, enemyLayers);

        foreach (RaycastHit2D enemy in raycastHit2D)
        {
            Debug.Log("Ranged hit in " + enemy.collider.name);

            enemy.collider.GetComponent<Enemy>().TakeDamage(damage);
        }

        Destroy(gameObject, 2f);
    }
}
