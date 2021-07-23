using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider rightWheel;
    public WheelCollider leftWheel;

    public Transform visRightWheel;
    public Transform visLeftWheel;

    public bool steering;
    public bool motor;
}