using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Controls")]
    public float Accel;
    public float Brake;
    public float Steering;

    [Header("Vehicle Settings")]
    public float EnginPower = 250f;
    public float BrakeForce = 1500f;
    public float SteerAngle = 35f;

    [Header("Wheels")]
    public WheelCollider[] FrontWheels;
    public WheelCollider[] RearWheels;

    public Vector3 COM;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = COM;
    }

    // Update is called once per frame
    void Update()
    {
        Accel = Input.GetAxis("Vertical");
        Brake = Input.GetAxis("Jump");
        Steering = Input.GetAxis("Horizontal");

        foreach (var wheel in FrontWheels)
        {
            wheel.motorTorque = EnginPower * Accel;
        }
        foreach (var wheel in RearWheels)
        {
            wheel.motorTorque = EnginPower * Accel;
        }

        foreach (var wheel in FrontWheels)
        {
            wheel.brakeTorque = BrakeForce * Brake;
        }
        foreach (var wheel in RearWheels)
        {
            wheel.brakeTorque = BrakeForce * Brake;
        }

        foreach (var wheel in FrontWheels)
        {
            wheel.steerAngle = SteerAngle * Steering;
        }
    }
}
