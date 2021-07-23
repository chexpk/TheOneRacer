using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{

    [Header("Controls")]
    public float accel;
    public float brake;
    public float steering;

    [Header("Vehicle Settings")]
    public float enginePower = 250f;
    public float brakeForce = 1500f;
    public float steerAngle = 45f;

    [Header("Wheels")]
    public WheelCollider[] frontWheels;
    public WheelCollider[] rearWheels;
    public WheelCollider frWheels;
    public WheelCollider flWheels;
    public WheelCollider rrWheels;
    public WheelCollider rlWheels;


    public Vector3 centreOfMass;

    public Transform frWheelTransform;
    public Transform flWheelTransform;
    public Transform rrWheelTransform;
    public Transform rlWheelTransform;

    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
         rb.centerOfMass = centreOfMass;
    }

    void Update()
    {
        RenderAllWheels();
    }

    void FixedUpdate()
    {
        Control();
        Engine();
    }

    void Control()
    {

        accel = Input.GetAxis("Vertical");
        brake = Input.GetAxis("Jump");
        steering = Input.GetAxis("Horizontal");

        foreach (var wheel in frontWheels)
        {
            wheel.motorTorque = enginePower * accel;
        }
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = enginePower * accel;
        }

        // foreach (var wheel in frontWheels)
        // {
        //     wheel.brakeTorque = brakeForce * brake;
        // }
        foreach (var wheel in rearWheels)
        {
            wheel.brakeTorque = brakeForce * brake;
        }

        foreach (var wheel in frontWheels)
        {
            wheel.steerAngle = steerAngle * steering;
        }
    }

    void RenderAllWheels()
    {
        RenderWheel(frWheelTransform, frWheels);
        RenderWheel(flWheelTransform, flWheels);
        RenderWheel(rrWheelTransform, rrWheels);
        RenderWheel(rlWheelTransform, rlWheels);
    }

    void RenderWheel(Transform wheelRender, WheelCollider wheelCollider)
    {
        wheelCollider.GetWorldPose(out var position, out var quaternion);
        wheelRender.position = position;
        wheelRender.rotation = quaternion;
    }

    void Engine()
    {

    }
}
