using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    public List<int> levelIndexList = new List<int>();

    private void Awake()
    {
        RandomizeList();
    }
    public void RandomizeList()
    {
        Shuffle(levelIndexList);
        StaticManager.levelIndexList = levelIndexList;
    }

    public static void Shuffle(List<int> list)
    {
        int n = list.Count -1;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        list.Reverse();
    }
}
