﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.LogWarning("Implementar cena de game over");
        //SceneManager.LoadScene("Game Over Scene");
    }

    public int getPlayerLife()
    {
        return life;
    }

    public void addLife(int amount)
    {
        life += amount;
    }

    public void removeLife(int amount)
    {
        life -= amount;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("Implementar o que acontece quando colidir com os inimigos");
    }
}