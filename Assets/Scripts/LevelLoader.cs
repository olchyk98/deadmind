using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        if (StaticManager.curLevelIndex >= StaticManager.levelIndexList.Count) return;
        SceneManager.LoadScene(StaticManager.levelIndexList[StaticManager.curLevelIndex]);
        StaticManager.curLevelIndex++;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
