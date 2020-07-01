using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 10;
    [SerializeField] GameObject[] sharks;
    private GameObject[] octopuses;
    [SerializeField] GameObject[] jellyfishes;

    private Rigidbody2D rb2D;
    private Enemy enemyScript;
    private Octopus octopusScript;
    private Jellyfish jellyfishScript;

    void Start()
    {
        octopuses = FindObjectsOfType<GameObject>();
        //octopuses[1].GetComponents<>
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        foreach (GameObject shark in sharks)
        {
            if (collision.gameObject == shark)
            {
                enemyScript = shark.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        foreach (GameObject octopus in octopuses)
        {
            if (collision.gameObject == octopus)
            {
                Debug.Log("HERE");
                octopusScript = octopus.GetComponent<Octopus>();
                octopusScript.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        foreach (GameObject jellyfish in jellyfishes)
        {
            if (collision.gameObject == jellyfish)
            {
                jellyfishScript = jellyfish.GetComponent<Jellyfish>();
                jellyfishScript.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 2f);
    }
}
