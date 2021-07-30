using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class GhostController : MonoBehaviour
{
    public Transform targetObject;
    public Transform ghostCar;
    public Transform visualWheelFR;
    public Transform visualWheelFL;
    public Transform visualWheelRR;
    public Transform visualWheelRL;

    public Ghost ghostScript;

    public float recordPeriod = 0.2f;
    public float nextActionTime = 0f;

    [SerializeField] Track ghostPointsInTime;
    [SerializeField] Track oldGhostPointsInTime;

    bool isRecord = false;
    bool isReplaying = false;
    bool isGhostPointReplaying = false;
    int indexInList = 0;

    float currTime = 0;

    void Start()
    {
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if (isReplaying)
        {
            Replay2Revenge();
        }

        if (isRecord)
        {
            RecordForGhost();
        }
    }

    private void FixedUpdate()
    {
        // if (isRecord)
        // {
        //     RecordForGhost();
        // }
    }

    void RecordForGhost()
    {
        // if (Time.time > nextActionTime)
        // {
        //     nextActionTime = Time.time + recordPeriod;
            var point = new PointInGhostTime(targetObject.position, targetObject.rotation, Time.time, visualWheelFR.localPosition, visualWheelFR.rotation, visualWheelFL.localPosition, visualWheelFL.rotation, visualWheelRR.localPosition, visualWheelRR.rotation, visualWheelRL.localPosition, visualWheelRL.rotation);
            // ghostPointsInTime.Add(new PointInGhostTime(targetObject.position, targetObject.rotation, Time.time));
            ghostPointsInTime.Add(point);
        // }
    }

    //TODO создать подсчет времени внутри трека в воспроизведении
    void ReplayGhost()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + recordPeriod;

            if (oldGhostPointsInTime.Count() - 2 > indexInList)
            {
                ghostScript.SetPointInGhostTime(oldGhostPointsInTime.GetPointByIndex(indexInList), oldGhostPointsInTime.GetPointByIndex(indexInList + 1), recordPeriod);
                indexInList += 1;
            }
            else
            {
                isReplaying = false;
            }
        }
    }

    void Replay2Revenge()
    {
        // ghostScript.SetParametersToGhostByTimeInTrack(currTime);
        ghostScript.SetAllParametersToGhostByTimeInTrack(currTime);
    }

    public void StartRecord()
    {
        if (!isRecord)
        {
            isRecord = true;
            nextActionTime = 0;
            ghostPointsInTime = new Track();
        }
        else
        {
            isReplaying = true;

            oldGhostPointsInTime = ghostPointsInTime;
            // Debug.Log($"количество точек {oldGhostPointsInTime.Count()} ");
            // Debug.Log(oldGhostPointsInTime.startWorldTime);
            // Debug.Log(oldGhostPointsInTime.GetTrackTime());

            ghostPointsInTime = new Track();
            indexInList = 0;

            ghostScript.SetTrack(oldGhostPointsInTime);
            currTime = 0;
        }
    }
}
