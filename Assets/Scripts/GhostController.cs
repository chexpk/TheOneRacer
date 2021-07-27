using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostController : MonoBehaviour
{
    public Transform targetObject;
    public Transform ghostCar;
    public Ghost ghostScript;

    public float recordPeriod = 0.5f;
    public float nextActionTime = 0f;

    List<PointInGhostTime> ghostPointsInTime;
    List<PointInGhostTime> oldGhostPointsInTime;
    List<List<PointInGhostTime>> allGhostPointsInTime;

    bool isRecord = false;
    bool isReplaying = false;
    bool isGhostPointReplaying = false;
    int indexInList = 0;

    // Transform targetPosition;
    // Quaternion targetRotation;

    void Start()
    {
        ghostPointsInTime = new List<PointInGhostTime>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isRecord)
            {
                isRecord = false;
                nextActionTime = 0;
            }
            else
            {
                isRecord = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isReplaying)
            {
                isReplaying = false;
            }
            else
            {
                isReplaying = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isReplaying)
        {
            ReplayGhost();
        }

        if (isRecord)
        {
            RecordForGhost();
        }
    }

    void RecordForGhost()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + recordPeriod;
            ghostPointsInTime.Add(new PointInGhostTime(targetObject.position, targetObject.rotation));
        }
    }

    void ReplayGhost()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + recordPeriod;

            if (oldGhostPointsInTime.Count - 2 > indexInList)
            {
                ghostScript.SetPointInGhostTime(oldGhostPointsInTime[indexInList], oldGhostPointsInTime[indexInList + 1], recordPeriod);
                indexInList += 1;
                // oldGhostPointsInTime.RemoveAt(0);
            }
            else
            {
                isReplaying = false;
                // ghostScript.SetReplaying(false);
            }
        }
    }

    public void StartRecord()
    {
        if (!isRecord)
        {
            isRecord = true;
            nextActionTime = 0;
        }
        else
        {
            isReplaying = true;

            oldGhostPointsInTime = ghostPointsInTime;
            Debug.Log(oldGhostPointsInTime.Count);

            ghostPointsInTime = new List<PointInGhostTime>();
            indexInList = 0;
        }
    }
}
