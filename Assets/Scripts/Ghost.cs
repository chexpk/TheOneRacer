using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    PointInGhostTime fromPoint;
    PointInGhostTime toPoint;
    float recordPeriod;

    bool isRepaying = false;
    float currTime = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        currTime += Time.deltaTime;
        if (isRepaying)
        {
            ReplayPoint();
        }

    }

    void ReplayPoint()
    {
        transform.position = Vector3.Lerp(fromPoint.position, toPoint.position, currTime / recordPeriod);
        transform.rotation = Quaternion.Lerp(fromPoint.rotation, toPoint.rotation, currTime / recordPeriod);
    }

    public void SetPointInGhostTime(PointInGhostTime from, PointInGhostTime to, float period)
    {
        fromPoint = from;
        toPoint = to;
        recordPeriod = period;
        SetReplaying(true);
        currTime = 0f;
    }

    public void SetReplaying(bool status)
    {
        isRepaying = status;
    }
}
