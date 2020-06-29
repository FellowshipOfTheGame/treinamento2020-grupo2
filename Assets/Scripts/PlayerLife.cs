using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 100;

    private Animator animator;
    private Player player;
    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
        }
    }

    private void Death()
    {
        player.StopMoving();
        animator.SetTrigger("Death");
        Debug.LogWarning("Implementar cena de game over");
        //SceneManager.LoadScene("Game Over Scene");
    }

    public int GetPlayerLife()
    {
        return life;
    }

    public void AddLife(int amount)
    {
        life += amount;
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        healthBar.SetHealth(life);

        if (life <= 0)
        {
            Death();
        }
    }
}
