using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(4f);
        }
        Destroy(gameObject);
    }
}
