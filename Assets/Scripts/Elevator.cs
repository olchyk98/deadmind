using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : Interactable
{
    private LevelLoader _levelLoader;
    private void Awake()
    {
        _levelLoader = GetComponent<LevelLoader>();
        OnInteract += _levelLoader.LoadNextLevel;
    }
    private void OnDestroy()
    {
        OnInteract -= _levelLoader.LoadNextLevel;
    }
}
