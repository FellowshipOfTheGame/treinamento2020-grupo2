using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform firePoint = default;
    [SerializeField] GameObject harpoonPrefab = null;
    [SerializeField] float shootTime = 1f;

    private bool canShoot = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (shootTime > 0 && canShoot)
        {
            Instantiate(harpoonPrefab, firePoint.position, firePoint.rotation);
            shootTime -= Time.deltaTime;
        }
        else
        {
            canShoot = false;
        }
    }
}
