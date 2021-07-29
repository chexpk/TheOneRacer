
using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class PointInGhostTime
{
    public Vector3 position;
    public Quaternion rotation;
    public float pointTime;

    public Vector3 positionFR;
    public Quaternion rotationFR;
    public Vector3 positionFL;
    public Quaternion rotationFL;
    public Vector3 positionRR;
    public Quaternion rotationRR;
    public Vector3 positionRL;
    public Quaternion rotationRL;

    public PointInGhostTime(Vector3 positionOfTarget, Quaternion rotationOfTarget, float timeOfPoint)
    {
        position = positionOfTarget;
        rotation = rotationOfTarget;
        pointTime = timeOfPoint;
    }

    public PointInGhostTime(Vector3 positionOfTarget, Quaternion rotationOfTarget, float timeOfPoint, Vector3 posFR, Quaternion rotFR)
    {
        position = positionOfTarget;
        rotation = rotationOfTarget;
        pointTime = timeOfPoint;

        positionFR = posFR;
        rotationFR = rotFR;
    }
}
