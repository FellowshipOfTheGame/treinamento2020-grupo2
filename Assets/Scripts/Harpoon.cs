using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 10;

    private List<GameObject> sharks;
    private List<GameObject> jellyfishes;
    private List<GameObject> octopuses;
    private Rigidbody2D rb2D;
    private List<Enemy> enemiesScripts;
    private List<Octopus> octopusesScripts;
    private List<Jellyfish> jellyfishesScripts;

    private void Awake()
    {
        octopuses = new List<GameObject>();
        octopusesScripts = new List<Octopus>();

        sharks = new List<GameObject>();
        enemiesScripts = new List<Enemy>();

        jellyfishes = new List<GameObject>();
        jellyfishesScripts = new List<Jellyfish>();
    }

    void Start()
    {
        var arrayOctopus = GameObject.FindGameObjectsWithTag("Octopus");
        var arraySharks = GameObject.FindGameObjectsWithTag("Shark");
        var arrayJellyfishes = GameObject.FindGameObjectsWithTag("Jellyfish");

        for (int i = 0; i < arrayOctopus.Length; i++)
        {
            octopuses.Add(arrayOctopus[i]);
            octopusesScripts.Add(octopuses[i].GetComponent<Octopus>());
        }

        for (int i = 0; i < arraySharks.Length; i++)
        {
            sharks.Add(arraySharks[i]);
            enemiesScripts.Add(sharks[i].GetComponent<Enemy>());
        }

        for (int i = 0; i < arrayJellyfishes.Length; i++)
        {
            jellyfishes.Add(arrayJellyfishes[i]);
            jellyfishesScripts.Add(jellyfishes[i].GetComponent<Jellyfish>());
        }

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
                enemiesScripts[sharks.IndexOf(shark)].TakeDamage(damage, ref enemiesScripts);
                Destroy(gameObject);
            }
        }
        foreach (GameObject octopus in octopuses)
        {
            if (collision.gameObject == octopus)
            {
                octopusesScripts[octopuses.IndexOf(octopus)].TakeDamage(damage, ref octopusesScripts);
                Destroy(gameObject);
            }
        }
        foreach (GameObject jellyfish in jellyfishes)
        {
            if (collision.gameObject == jellyfish)
            {
                jellyfishesScripts[jellyfishes.IndexOf(jellyfish)].TakeDamage(damage, ref jellyfishesScripts);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 2f);
    }
}
