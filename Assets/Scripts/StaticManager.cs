using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticManager : MonoBehaviour
{
    public static List<int> levelIndexList;
    public static int curLevelIndex;
    public static GameObject gameObject;
    private void Awake()
    {
        gameObject = GetComponent<GameObject>();
        DontDestroyOnLoad(this);
    }
    public static void DeleteObject()
    {
        Destroy(gameObject);
    }
}
