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
    bool isTimerWork;

    void Start()
    {

    }

    // Update is called once per frame
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
        lastTime.text = lastTrackTime.ToString("0.00");
    }

    public void UpdateCurrentTime()
    {
        currentTrackTime = 0f;
    }

    public void SetTimerStatus(bool status)
    {
        isTimerWork = status;
    }


}
