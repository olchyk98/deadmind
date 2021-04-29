using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityAction OnTimerStopped;
    public UnityAction<string> OnTimerUpdate;
    public string timerOutput; 
    public bool HasStarted { get; private set; }

    [SerializeField] private float timerStart;
    [SerializeField] private float timerEnd;
    [SerializeField] private bool countsUp;

    private float _timerSeconds;
    private const string OutputPattern = @"HH\:mm\:ss";

    private void Awake()
    {
        if (HasStarted)
        {
            ResetTimer();
        }
    }

    private void Update()
    {
        if (!HasStarted) return;
        
        _timerSeconds += countsUp ? Time.deltaTime : -Time.deltaTime;
        var t = TimeSpan.FromSeconds(_timerSeconds);
        timerOutput = t.ToString(OutputPattern);
        
        if (countsUp && _timerSeconds > timerEnd 
            || !countsUp && _timerSeconds < timerEnd)
        {
            StopTimer();
        }
        
        OnTimerUpdate?.Invoke(timerOutput);
    }

    public void StartTimer()
    {
        ResetTimer();
        HasStarted = true;
    }

    public void StopTimer()
    {
        HasStarted = false;
        OnTimerStopped?.Invoke();
    }
    
    private void ResetTimer()
    {
        _timerSeconds = timerStart;
    }
}
