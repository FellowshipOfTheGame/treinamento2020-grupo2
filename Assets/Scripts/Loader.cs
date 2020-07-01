using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private SceneLoader scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
        scene.LoadMenu();
    }
}
