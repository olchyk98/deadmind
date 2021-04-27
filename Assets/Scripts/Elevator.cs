using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : Interactable
{
    LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
        OnInteract += levelLoader.LoadNextLevel;
    }
    private void OnDestroy()
    {
        OnInteract -= levelLoader.LoadNextLevel;
    }
}
