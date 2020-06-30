using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemiesPrefab;

    void Start()
    {
        //StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        foreach (GameObject enemy in enemiesPrefab)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(10f);
    }
}
