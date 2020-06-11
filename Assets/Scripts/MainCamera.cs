using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform characterTransform = null;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.2f;

    Vector3 offset = new Vector3(0, 0, -15);

    void Update()
    {
        Vector3 desiredPosition = characterTransform.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        transform.position = smoothPosition;
    }
}
