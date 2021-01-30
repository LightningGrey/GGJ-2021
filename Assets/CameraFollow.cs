using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3 (0.0f, 0.1f, 0.0f);

    private Vector3 velocity = Vector3.zero;

    public float smoothing = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
        transform.position = smoothedPos;
    }
}
