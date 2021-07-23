using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float rotationSpeed;

    Quaternion startRotation;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, moveSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation * startRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
