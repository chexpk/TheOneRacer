using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Track
{
    public List<PointInGhostTime> track = new List<PointInGhostTime>();
    public float startWorldTime = -1;

    public void Add(PointInGhostTime point)
    {
        var position = point.position;
        var rotation = point.rotation;
        if (startWorldTime < 0)
        {
            startWorldTime = point.pointTime;
        }
        var pointTime = point.pointTime - startWorldTime;
        track.Add(new PointInGhostTime(position, rotation, pointTime));
    }

    public int Count()
    {
        return track.Count;
    }

    public PointInGhostTime GetPointByIndex(int index)
    {
        return track[index];
    }

    public void SetStartWorldTime(float time)
    {
        startWorldTime = time;
    }

    public float GetTrackTime()
    {
         return track[Count() - 1].pointTime;
    }

    public bool GetGhostParametersInPointTime(float timeInTrack, out Vector3 position, out Quaternion rotation)
    {
        position = new Vector3(0,0,0);
        rotation = new Quaternion(0,0,0, 0);

        PointInGhostTime fromPoint;
        PointInGhostTime toPoint;
        if (!GetNearestPoints(timeInTrack, out fromPoint, out toPoint))
        {
            Debug.Log("не нашел в треке поинты");
            return false;
        }

        if (track[track.Count - 1].pointTime < timeInTrack)
        {
            return false;
        }

        float durationTimeBetweenPoints = - toPoint.pointTime + fromPoint.pointTime;
        float timeInPoint = fromPoint.pointTime - timeInTrack;

        position = Vector3.Lerp(fromPoint.position, toPoint.position, timeInPoint / durationTimeBetweenPoints);
        rotation = Quaternion.Lerp(fromPoint.rotation, toPoint.rotation, timeInPoint / durationTimeBetweenPoints);

        return true;
    }

    private bool GetNearestPoints(float timeInTrack, out PointInGhostTime fromPoint, out PointInGhostTime toPoint)
    {
        fromPoint = null;
        toPoint = null;

        for (int i = 1; i <= track.Count() - 1; i++)
        {
            if (timeInTrack <= track[i].pointTime)
            {
                if (timeInTrack > GetPointByIndex(i - 1).pointTime)
                {
                    fromPoint = track[i - 1];
                    toPoint = track[i];
                    return true;
                }
            }
        }
        return false;
    }
}
