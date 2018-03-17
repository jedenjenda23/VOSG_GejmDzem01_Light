using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float cameraSmoothing = 5f;

    private void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, cameraSmoothing * Time.deltaTime);
    }
}
