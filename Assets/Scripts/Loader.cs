using UnityEngine;

public class Loader : MonoBehaviour
{
    private SceneLoader scene;

    void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
        scene.LoadMenu();
    }
}
