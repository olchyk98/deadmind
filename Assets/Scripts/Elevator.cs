using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable
{
    LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
    }
    public override void Hit()
    {
        base.Hit();
        levelLoader.LoadNextLevel();
    }
}
