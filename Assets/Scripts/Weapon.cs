using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint = default;
    [SerializeField] private GameObject harpoonPrefab = null;
    [SerializeField] private float shootTime = 5f;

    private Animator animator;
    private Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.GetCanMove())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("RangedAttack");
            }
        }
    }

    public void Shoot()
    {
        Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
    }
}
