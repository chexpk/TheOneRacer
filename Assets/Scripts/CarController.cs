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

    [Header("Smoke From Tiers")]
    public float minSpeedForSmoke;
    public float minAngleForSmoke;
    public ParticleSystem[] tireSmokeEffects;

    [Header("Trace From Tiers")]
    public float minSidewaysSlip = 0.8f;
    public float minForwardSlip = 1f;
    public GameObject traceTestObject;
    public TrailRenderer trailFR;
    public TrailRenderer trailFL;
    public TrailRenderer trailRL;
    public TrailRenderer trailRR;

    [Range(0,1)]
    public float steerHelpValue = 0;

    float horInput;
    float vertInput;
    bool isOnground;
    float lastYRotation;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        CheckOnGround();
        Accelerate();
        NitroManager();
        ManageHardBreak();
        EmitSmokeTiers();
        EmitTraceTiers();
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

            VisualWheelsToColliders(axle.rightWheel, axle.visRightWheel);
            VisualWheelsToColliders(axle.leftWheel, axle.visLeftWheel);
        }
    }

    void VisualWheelsToColliders(WheelCollider col, Transform visWheel)
    {
        col.GetWorldPose(out var position, out var rotation);

        visWheel.position = position;
        visWheel.rotation = rotation;
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

    void EmitSmokeTiers() //GetGroundHit(out WheelHit hit) - чекнуть через этот метод
    {
        if (rb.velocity.magnitude > minSpeedForSmoke)
        {
            float angle = Quaternion.Angle(Quaternion.LookRotation(rb.velocity, Vector3.up), Quaternion.LookRotation(transform.forward, Vector3.up));
            if (angle > minAngleForSmoke && angle < 160 && isOnground)
            {
                SwitchSmokeParticles(true);
            }
            else
            {
                SwitchSmokeParticles(false);
            }
        }
        else
        {
            SwitchSmokeParticles(false);
        }
    }

    void EmitTraceTiers()
    {
        // wheelCol.GetGroundHit(out WheelHit hit);
        foreach (var wheelCol in wheelColliders)
        {
            wheelCol.GetGroundHit(out var hit);

            if (hit.sidewaysSlip > minSidewaysSlip || hit.sidewaysSlip < -minSidewaysSlip)
            {
                // var trace = Instantiate(traceTestObject, hit.point, Quaternion.identity);
                // Destroy(trace, 5f);
                Debug.Log("заносБОКОМ");
                SwitchTraceParticles(true);

            }
            else
            {
                SwitchTraceParticles(false);
            }
            if (hit.forwardSlip > minForwardSlip || hit.forwardSlip < -minForwardSlip)
            {
                // var trace = Instantiate(traceTestObject, hit.point, Quaternion.identity);
                // Destroy(trace, 5f);
                // Debug.Log("заносПрямо");
            }
        }
    }

    void SwitchSmokeParticles(bool enable)
    {
        foreach (var ps in tireSmokeEffects)
        {
            ParticleSystem.EmissionModule psEm = ps.emission;
            psEm.enabled = enable;
        }
    }

    void SwitchTraceParticles(bool enable)
    {
        trailRL.emitting = enable;
        Debug.Log($"меняем на {enable}");
    }
}
