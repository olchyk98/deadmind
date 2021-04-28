using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    public static List<int> levelIndexList;
    public static int curLevelIndex;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
