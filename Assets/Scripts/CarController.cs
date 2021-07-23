using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public AxleInfo[] carAxis = new AxleInfo[2];

    public float carSpeed;
    public float steerAngle;

    private float horInput;
    private float vertInput;


    private void FixedUpdate()
    {
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        Accelerate();
    }

    void Accelerate()
    {
        foreach (var axle in carAxis)
        {
            if (axle.steering)
            {
                axle.rightWheel.steerAngle = steerAngle * horInput;
                axle.leftWheel.steerAngle = steerAngle * horInput;
            }

            if (axle.motor)
            {
                axle.rightWheel.motorTorque = carSpeed * vertInput;
                axle.leftWheel.motorTorque = carSpeed * vertInput;
            }
        }
    }
}
