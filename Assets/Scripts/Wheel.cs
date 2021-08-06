using System;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] WheelCollider wheelCollider;
    [SerializeField]  Transform visWheel;

    [Header("Smoke From Tier")]
    [SerializeField]  ParticleSystem smokeParticle;
    [SerializeField]  float minSidewaysSlipForSmoke = 0.8f;
    [SerializeField]  float minForwardSlipForSmoke = 0.9f;

    [Header("Trace From Tier")]
    [SerializeField]  TrailRenderer trailRenderer;
    [SerializeField]  float minSidewaysSlipForTrace = 1f;
    [SerializeField]  float minForwardSlipForTrace = 1f;

    private void Update()
    {
        EmitTraceTiers();
        EmitSmokeTiers();
        VisualWheelsToCollider();
    }

    void VisualWheelsToCollider()
    {
        wheelCollider.GetWorldPose(out var position, out var rotation);
        visWheel.position = position;
        visWheel.rotation = rotation;
    }

    void EmitSmokeTiers()
    {
        if (wheelCollider.GetGroundHit(out var hit))
        {
            if (hit.sidewaysSlip > minSidewaysSlipForSmoke || hit.sidewaysSlip < -minSidewaysSlipForSmoke || hit.forwardSlip > minForwardSlipForSmoke || hit.forwardSlip < -minForwardSlipForSmoke)
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
        if (wheelCollider.GetGroundHit(out var hit))
        {
            if (hit.sidewaysSlip > minSidewaysSlipForTrace || hit.sidewaysSlip < -minSidewaysSlipForTrace || hit.forwardSlip > minForwardSlipForTrace || hit.forwardSlip < -minForwardSlipForTrace)
            {
                SwitchTraceParticles(true);
            }
            else
            {
                SwitchTraceParticles(false);
            }
        }
        else
        {
            SwitchTraceParticles(false);
        }
    }

    void SwitchSmokeParticles(bool enable)
    {
        ParticleSystem.EmissionModule psEm = smokeParticle.emission;
        psEm.enabled = enable;
    }

    void SwitchTraceParticles(bool enable)
    {
        trailRenderer.emitting = enable;
    }
}
