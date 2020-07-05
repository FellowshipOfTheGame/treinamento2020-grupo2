using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 100;
    [SerializeField] private HealthBar healthBar;

    private Animator animator;
    private Player player;
    private SceneLoader sceneLoader;

    public bool isDead 
    { 
        get 
        { 
            return life <= 0; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
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
        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(3);
        sceneLoader.LoadGameOverScene();
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
