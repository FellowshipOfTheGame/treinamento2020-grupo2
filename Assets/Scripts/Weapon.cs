using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint = default;
    [SerializeField] private GameObject harpoonPrefab = null;
    [SerializeField] private float shootTime = 5f;

    //private float nextRangedAttackTime = 0f;
    private float firstFrameTime;

    private Animator animator;
    private Player player;

    private void Start()
    {
        firstFrameTime = Time.time;
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
                /*
                if (Time.time - firstFrameTime >= shootTime)
                {
                    firstFrameTime = Time.time;
                    Shoot();
                }*/
            }
        }
    }

    public void Shoot()
    {
        Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
    }
}
