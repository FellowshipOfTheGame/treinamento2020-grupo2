using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint = default;
    [SerializeField] private GameObject harpoonPrefab = null;
    [SerializeField] private float shootTime = 5f;

    private float nextRangedAttackTime = 0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time >= nextRangedAttackTime)
            {
                Shoot();
                nextRangedAttackTime = Time.time + 1f / shootTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            animator.SetBool("RangedAttack", false);
        }
    }

    private void Shoot()
    {
        animator.SetBool("RangedAttack", true);
        Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
    }
}
