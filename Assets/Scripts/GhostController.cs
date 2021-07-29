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

    Track ghostPointsInTime;
    Track oldGhostPointsInTime;

    bool isRecord = false;
    bool isReplaying = false;
    bool isGhostPointReplaying = false;
    int indexInList = 0;

    float currTime = 0;

    // Transform targetPosition;
    // Quaternion targetRotation;

    void Start()
    {
        // ghostPointsInTime = new Track();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     if (isRecord)
        //     {
        //         isRecord = false;
        //         nextActionTime = 0;
        //     }
        //     else
        //     {
        //         isRecord = true;
        //     }
        // }
        //
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     if (isReplaying)
        //     {
        //         isReplaying = false;
        //     }
        //     else
        //     {
        //         isReplaying = true;
        //     }
        // }

        currTime += Time.deltaTime;
        if (isReplaying)
        {
            Replay2Revenge();
        }

    }

    private void FixedUpdate()
    {
        // if (isReplaying)
        // {
        //     ReplayGhost();
        // }

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
            ghostPointsInTime.Add(new PointInGhostTime(targetObject.position, targetObject.rotation, Time.time));
        }
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
                // oldGhostPointsInTime.RemoveAt(0);
            }
            else
            {
                isReplaying = false;
                // ghostScript.SetReplaying(false);
            }
        }
    }

    void Replay2Revenge()
    {
        ghostScript.SetParametersToGhostByTimeInTrack(currTime);
    }

    public void StartRecord()
    {
        //TODO ошибка - появляется пустой масссив на воспроизведение
        if (!isRecord)
        {
            isRecord = true;
            nextActionTime = 0;
            ghostPointsInTime = new Track();
            // задать значение стартового времени?
        }
        else
        {
            isReplaying = true;

            oldGhostPointsInTime = ghostPointsInTime;
            Debug.Log($"количество точек {oldGhostPointsInTime.Count()} ");
            Debug.Log(oldGhostPointsInTime.startWorldTime);
            Debug.Log(oldGhostPointsInTime.GetTrackTime());

            ghostPointsInTime = new Track();
            indexInList = 0;

            ghostScript.SetTrack(oldGhostPointsInTime);
            currTime = 0;
        }
    }
}
