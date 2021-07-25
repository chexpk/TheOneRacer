using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostController : MonoBehaviour
{
    public Transform targetObject;
    public GameObject ghostCar;

    public float recordPeriod = 0.5f;
    public float nextActionTime = 0f;

    List<PointInGhostTime> ghostPointsInTime;

    bool isRecord = false;
    bool isReplaying = false;

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
            if (ghostPointsInTime.Count > 0)
            {
                PointInGhostTime pointInTime = ghostPointsInTime[0];
                ghostCar.transform.position = pointInTime.position;
                // Debug.Log(pointInTime.position);
                ghostCar.transform.rotation = pointInTime.rotation;
                // Debug.Log(pointInTime.rotation);
                ghostPointsInTime.RemoveAt(0);
            }
            else
            {
                isReplaying = false;
            }
        }

    }
}
