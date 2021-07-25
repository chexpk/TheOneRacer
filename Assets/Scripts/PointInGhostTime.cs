
using UnityEngine;
using UnityEngine.UIElements;

public class PointInGhostTime
{
    public Vector3 position;
    public Quaternion rotation;

    public PointInGhostTime(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}
