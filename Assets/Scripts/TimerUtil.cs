using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimerUtil : MonoBehaviour
{
    public UnityAction OnTimerStopped;
    public UnityAction<string> OnTimerUpdate;
    public string timerOutput; 
    public bool HasStarted { get; private set; }

    [SerializeField] private float timerStart;
    [SerializeField] private float timerEnd;
    [SerializeField] private bool countsUp;

    [SerializeField]private TextMeshProUGUI timerText;
    private float _timerSeconds;
    private const string OutputPattern = @"mm\:ss";

    private void Awake()
    {
        SceneManager.sceneLoaded += FindTimerText;
        FindTimerText(SceneManager.GetActiveScene(), LoadSceneMode.Additive);
        DontDestroyOnLoad(this);
    }
    private void FindTimerText(Scene scene, LoadSceneMode mode)
    {
        timerText = GameObject.FindWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!HasStarted) return;
        
        _timerSeconds += countsUp ? Time.deltaTime : -Time.deltaTime;
        var t = TimeSpan.FromSeconds(_timerSeconds);
        timerOutput = t.ToString(OutputPattern);
        timerText.text = timerOutput;
        
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
