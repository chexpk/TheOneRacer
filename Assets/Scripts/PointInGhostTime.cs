
using UnityEngine;
using UnityEngine.UIElements;

public class PointInGhostTime
{
    public Vector3 position;
    public Quaternion rotation;
    public float pointTime;

    public PointInGhostTime(Vector3 positionOfTarget, Quaternion rotationOfTarget, float timeOfPoint)
    {
        position = positionOfTarget;
        rotation = rotationOfTarget;
        pointTime = timeOfPoint;
    }
}
