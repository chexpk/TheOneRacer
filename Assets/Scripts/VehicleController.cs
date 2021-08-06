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

    public float rpmEngine;

    public float rpmRight;
    public float rpmLeft;

    public float motorTorque;


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
            wheel.motorTorque = motorTorque * accel * 5;
        }
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = motorTorque * accel * 5;
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
        rpmRight = rrWheels.rpm;
        rpmLeft = rlWheels.rpm;

        if (rpmRight >= rpmLeft)
        {
            rpmEngine = rpmRight;
        }
        else
        {
            rpmEngine = rpmLeft;
        }

        if (rpmEngine < 2700f)
        {
            motorTorque = 100f;
        }
        if (rpmEngine > 2700f && rpmEngine < 3200f)
        {
            motorTorque = 0.29f * rpmEngine - 683;
        }
        if (rpmEngine > 3200f && rpmEngine < 3700f)
        {
            motorTorque = (-0.09f) * rpmEngine - 43;
        }
        if (rpmEngine > 3700f && rpmEngine < 4200f)
        {
            motorTorque = (-0.01f) * rpmEngine + 372;
        }
        if (rpmEngine > 4200f && rpmEngine < 4700f)
        {
            motorTorque = 0.01f * rpmEngine + 243;
        }
        if (rpmEngine > 4700f && rpmEngine < 5200f)
        {
            motorTorque = (-0.02f) * rpmEngine + 384;
        }
        if (rpmEngine > 5200f && rpmEngine < 5700f)
        {
            motorTorque = (-0.01f) * rpmEngine + 800;
        }
        if (rpmEngine > 5700f && rpmEngine < 6200f)
        {
            motorTorque = (-0.01f) * rpmEngine + 800;
        }
        if (rpmEngine > 6200f && rpmEngine < 9000f)
        {
            motorTorque = (-0.06f) * rpmEngine + 552;
        }
    }
}
