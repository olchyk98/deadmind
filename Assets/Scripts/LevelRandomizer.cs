using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    public List<int> levelIndexList = new List<int>();

    private void Start()
    {
        RandomizeList();
    }
    public void RandomizeList()
    {
        Shuffle(ref levelIndexList);
        StaticManager.levelIndexList = levelIndexList;
    }

    public static void Shuffle(ref List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
