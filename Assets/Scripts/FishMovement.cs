using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] GameObject[] walls;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject wall in walls)
        {
            if (collision.gameObject == wall)
            {
                Destroy(gameObject);
            }
        }
    }
}
