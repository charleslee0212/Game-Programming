﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosistion = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosistion, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
