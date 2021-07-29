using System;
using System.Collections.Generic;
using UnityEngine;

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

    public void GetGhostParametersInPointTime(float timeInTrack, out Vector3 position, out Quaternion rotation)
    {
        int fromPointIndex;
        int toPointIndex;
        GetNearestPoints(timeInTrack,  out fromPointIndex, out toPointIndex);

        var fromPoint = GetPointByIndex(fromPointIndex);
        var toPoint = GetPointByIndex(toPointIndex);

        float durationTimeBetweenPoints = toPoint.pointTime - fromPoint.pointTime;

        position = Vector3.Lerp(fromPoint.position, toPoint.position, timeInTrack / durationTimeBetweenPoints);
        rotation = Quaternion.Lerp(fromPoint.rotation, toPoint.rotation, timeInTrack / durationTimeBetweenPoints);

        if (fromPointIndex == 0 && toPointIndex == 0)
        {
            Debug.Log("ошибка");
        }
    }

    private void GetNearestPoints(float timeInTrack, out int fromPointIndex, out int toPointIndex)
    {
        int from = 0;
        int to = 0;

        int i = 0;
        foreach (var point in track)
        {
            if (timeInTrack <= point.pointTime)
            {
                if (timeInTrack > GetPointByIndex(i - 1).pointTime)
                {
                    from = i - 1;
                    to = i;
                }
                else
                {
                    from = i;
                    to = i + 1;
                }
            }
            i += 1;
        }

        fromPointIndex = from;
        toPointIndex = to;

    }
}
