using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform visualWheelFR;
    public Transform visualWheelFL;
    public Transform visualWheelRR;
    public Transform visualWheelRL;

    PointInGhostTime fromPoint;
    PointInGhostTime toPoint;
    float recordPeriod;

    bool isRepaying = false;
    float currTime = 0f;

    Track currentTrack;

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

    public void SetTrack(Track track)
    {
        currentTrack = track;
    }

    public void SetParametersToGhostByTimeInTrack(float timeInTrack)
    {
        if (!currentTrack.GetGhostParametersInPointTime(timeInTrack, out var position, out var rotation))
        {
            Debug.Log("нет позиции");
            return;
        }
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetAllParametersToGhostByTimeInTrack(float timeInTrack)
    {
        if (!currentTrack.GetAllGhostParametersInPointTime(timeInTrack, out var position, out var rotation, out var posFR, out var rotFR, out var posFL, out var rotFL, out var posRR, out var rotRR, out var posRL, out var  rotRL))
        {
            // Debug.Log("нет позиции");
            return;
        }
        transform.position = position;
        transform.rotation = rotation;

        visualWheelFR.localPosition = posFR;
        visualWheelFR.rotation = rotFR;
        visualWheelFL.localPosition = posFL;
        visualWheelFL.rotation = rotFL;
        visualWheelRR.localPosition = posRR;
        visualWheelRR.rotation = rotRR;
        visualWheelRL.localPosition = posRL;
        visualWheelRL.rotation = rotRL;
    }
}
