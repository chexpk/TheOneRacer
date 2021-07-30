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

        var positionFR = point.positionFR;
        var rotationFR = point.rotationFR;
        var positionFL = point.positionFL;
        var rotationFL = point.rotationFL;

        var positionRR = point.positionRR;
        var rotationRR = point.rotationRR;

        var positionRL = point.positionRL;
        var rotationRL = point.rotationRL;

        var pointTime = point.pointTime - startWorldTime;
        track.Add(new PointInGhostTime(position, rotation, pointTime, positionFR, rotationFR, positionFL, rotationFL, positionRR, rotationRR, positionRL, rotationRL));
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

    public bool GetAllGhostParametersInPointTime(float timeInTrack, out Vector3 position, out Quaternion rotation, out Vector3 posFR, out Quaternion rotFR, out Vector3 posFL, out Quaternion rotFL, out Vector3 posRR, out Quaternion rotRR, out Vector3 posRL, out Quaternion rotRL)
    {
        position = new Vector3(0,0,0);
        rotation = new Quaternion(0,0,0, 0);

        posFR = new Vector3(0,0,0);
        rotFR = new Quaternion(0,0,0, 0);
        posFL = new Vector3(0,0,0);
        rotFL = new Quaternion(0,0,0, 0);
        posRR = new Vector3(0,0,0);
        rotRR = new Quaternion(0,0,0, 0);
        posRL = new Vector3(0,0,0);
        rotRL = new Quaternion(0,0,0, 0);

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

        posFR = Vector3.Lerp(fromPoint.positionFR, toPoint.positionFR, timeInPoint / durationTimeBetweenPoints);
        rotFR = Quaternion.Lerp(fromPoint.rotationFR, toPoint.rotationFR, timeInPoint / durationTimeBetweenPoints);
        // if (IsQuaternionInvalid(fromPoint.rotationFR) || IsQuaternionInvalid(toPoint.rotationFR))
        // {
        //     rotFR = Quaternion.Lerp(fromPoint.rotationFR, toPoint.rotationFR, timeInPoint / durationTimeBetweenPoints);
        // }

        posFL = Vector3.Lerp(fromPoint.positionFL, toPoint.positionFL, timeInPoint / durationTimeBetweenPoints);
        rotFL = Quaternion.Lerp(fromPoint.rotationFL, toPoint.rotationFL, timeInPoint / durationTimeBetweenPoints);
        // if (IsQuaternionInvalid(fromPoint.rotationFL) || IsQuaternionInvalid(toPoint.rotationFL))
        // {
        //     rotFL = Quaternion.Lerp(fromPoint.rotationFL, toPoint.rotationFL, timeInPoint / durationTimeBetweenPoints);
        // }

        posRR = Vector3.Lerp(fromPoint.positionRR, toPoint.positionRR, timeInPoint / durationTimeBetweenPoints);
        rotRR = Quaternion.Lerp(fromPoint.rotationRR, toPoint.rotationRR, timeInPoint / durationTimeBetweenPoints);
        // if (IsQuaternionInvalid(fromPoint.rotationRR) || IsQuaternionInvalid(toPoint.rotationRR))
        // {
        //     rotRR = Quaternion.Lerp(fromPoint.rotationRR, toPoint.rotationRR, timeInPoint / durationTimeBetweenPoints);
        // }

        posRL = Vector3.Lerp(fromPoint.positionRL, toPoint.positionRL, timeInPoint / durationTimeBetweenPoints);
        rotRL = Quaternion.Lerp(fromPoint.rotationRL, toPoint.rotationRL, timeInPoint / durationTimeBetweenPoints);
        // if (IsQuaternionInvalid(fromPoint.rotationRL) || IsQuaternionInvalid(toPoint.rotationRL))
        // {
        //     rotRL = Quaternion.Lerp(fromPoint.rotationRL, toPoint.rotationRL, timeInPoint / durationTimeBetweenPoints);
        // }

        return true;
    }

    bool IsQuaternionInvalid(Quaternion q) {
        bool check = q.x == 0f;
        check &= q.y == 0;
        check &= q.z == 0;
        check &= q.w == 0;

        return check;
    }
}
