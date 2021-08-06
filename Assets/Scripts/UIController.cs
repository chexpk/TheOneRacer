using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] float currentTrackTime = 0f;
    [SerializeField] float lastTrackTime;
    [SerializeField] Text lastTime;
    [SerializeField] Text currentTime;
    [SerializeField] Text bestTime;
    int numberOfTrack = 1;
    bool isTimerWork;
    [SerializeField] private List<float> allTracks;

    void Start()
    {
        lastTime.text = "";
        allTracks = new List<float>();
    }

    void Update()
    {
        if (!isTimerWork) return;
        currentTrackTime += Time.deltaTime;
        currentTime.text = currentTrackTime.ToString("0.00");
    }

    public void UpdateLastTime(float timeFromReplayingTrack)
    {
        UpdateCurrentTime();
        lastTrackTime = timeFromReplayingTrack;
        AddTimeToList(lastTrackTime);
        lastTime.text += ($"{numberOfTrack}. " +lastTrackTime.ToString("0.00") + "\n");
        numberOfTrack += 1;
        ShowMinimalTime();
    }

    public void SetTimerStatus(bool status)
    {
        isTimerWork = status;
    }

    void UpdateCurrentTime()
    {
        currentTrackTime = 0f;
    }

    void AddTimeToList(float time)
    {
        allTracks.Add(time);
    }

    void ShowMinimalTime()
    {
        var time = GetMinimalTime();
        bestTime.text = time.ToString("0.00");
    }

    float GetMinimalTime()
    {
        float result = 0;
        foreach (var time in allTracks)
        {
            if (result == 0)
            {
                result = time;
            }

            if (time < result)
            {
                result = time;
            }
        }
        return result;
    }
}
