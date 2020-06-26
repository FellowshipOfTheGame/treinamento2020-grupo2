using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform firePoint = default;
    [SerializeField] GameObject harpoonPrefab = null;
    [SerializeField] float shootTime = 1f;

    private Animator animator;
    private bool canShoot = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            animator.SetBool("RangedAttack", false);
        }
    }

    private void Shoot()
    {
        if (shootTime > 0 && canShoot)
        {
            animator.SetBool("RangedAttack", true);
            Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
            shootTime -= Time.deltaTime;
        }
        else
        {
            canShoot = false;
        }
    }
}
