using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    public static List<int> levelIndexList;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
