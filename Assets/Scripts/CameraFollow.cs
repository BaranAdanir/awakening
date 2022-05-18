using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.9f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.position = smoothedPosition;
    }



}
