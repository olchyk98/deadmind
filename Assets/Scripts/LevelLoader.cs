using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(StaticManager.levelIndexList[StaticManager.curLevelIndex]);
        StaticManager.curLevelIndex++;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
