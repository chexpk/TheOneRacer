using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public AxleInfo[] carAxis = new AxleInfo[2];
    public WheelCollider[] wheelColliders;

    public float carSpeed;
    public float steerAngle;
    public Transform centerOfMass;
    public float nitroPower;
    public GameObject nitroEffects;

    [Range(0,1)]
    public float steerHelpValue = 0;

    float horInput;
    float vertInput;
    bool isOnground;
    float lastYRotation;
    Rigidbody rb;

    public AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        float speed = rb.velocity.magnitude;
        audioSource.pitch = (speed / 2) + 0.1f;
    }

    private void FixedUpdate()
    {
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        CheckOnGround();



        Accelerate();
        NitroManager();
        ManageHardBreak();
        SteerHelper();
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

    void SteerHelper()
    {
        if(!isOnground) return;

        if(Mathf.Abs(transform.rotation.eulerAngles.y - lastYRotation) < 10f)
        {
            float turnAdjust = (transform.rotation.eulerAngles.y - lastYRotation) * steerHelpValue;
            Quaternion rotateHelp = Quaternion.AngleAxis(turnAdjust, Vector3.up);
            rb.velocity = rotateHelp * rb.velocity;
        }
        lastYRotation = transform.rotation.eulerAngles.y;
    }

    void CheckOnGround()
    {
        isOnground = true;
        foreach (var wheelCol in wheelColliders)
        {
            if (!wheelCol.isGrounded)
            {
                isOnground = false;
            }
        }
    }

    void NitroManager()
    {
        if (Input.GetKey(KeyCode.LeftShift) && vertInput > 0.01f)
        {
            rb.AddForce(transform.forward * nitroPower);
            nitroEffects.SetActive(true);
        }
        else
        {
            if (nitroEffects.activeSelf)
            {
                nitroEffects.SetActive(false);
            }
        }
    }

    void ManageHardBreak()
    {
        foreach(var axle in carAxis)
        {
            if (!axle.hardBreak) continue;
            if(Input.GetKey(KeyCode.Space))
            {
                axle.rightWheel.brakeTorque = 50000;
                axle.leftWheel.brakeTorque = 50000;
            }
            else
            {
                axle.rightWheel.brakeTorque = 0;
                axle.leftWheel.brakeTorque = 0;
            }
        }
    }
}
