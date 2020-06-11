using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Xspeed = 5f;
    [SerializeField] float Yspeed = 4f;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(inputX * Xspeed * Time.deltaTime, inputY * Yspeed * Time.deltaTime, 0));
    }
}
