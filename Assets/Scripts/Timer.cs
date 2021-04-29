using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityAction TimerStopped;
    public string timerOutput;
    public bool started;

    [SerializeField] private float timerStart;
    [SerializeField] private float timerEnd;
    [SerializeField] private bool countsUp;
    
    Stopwatch watch = new Stopwatch();

    private float timerSeconds;
    private string pattern = @"HH\:mm\:ss";
    
    private void Awake()
    {
        if (started)
        {
            ResetTimer();
        }
    }

    private void Update()
    {
        if (started)
        {
            timerSeconds += countsUp ? Time.deltaTime : -Time.deltaTime;
            if (countsUp && timerSeconds > timerEnd)
            {
                StopTimer();
            }else if (!countsUp && timerSeconds < timerEnd)
            {
                StopTimer();
            }

            TimeSpan t = TimeSpan.FromSeconds(timerSeconds);
            timerOutput = string.Format(pattern, t);
        }
    }

    public void ResetTimer()
    {
        timerSeconds = timerStart;
    }

    public void StartTimer()
    {
        started = true;
        
    }

    public void StopTimer()
    {
        started = false;
        TimerStopped.Invoke();
    }
}
