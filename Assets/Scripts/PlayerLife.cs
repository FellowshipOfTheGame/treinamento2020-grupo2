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
    [SerializeField] private HealthBar healthBar;
    private bool isDead 
    { 
        get 
        { 
            return life <= 0; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        healthBar.SetMaxHealth(life);
        animator.SetBool("IsDead", false);
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
        animator.SetBool("IsDead", true);
        Debug.LogWarning("Implementar cena de game over");
        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game Over Scene");
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

        if (isDead)
        {
            Death();
        }
    }
}
