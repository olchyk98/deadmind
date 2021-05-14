using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private TimerUtil _timer;
    private Animation fadeObjectAnim;
    private GameObject deathMenu;
    private AudioSource _audioSource;
    private void Start()
    {
        _timer = GetComponent<TimerUtil>();
        _audioSource = GetComponent<AudioSource>();
        fadeObjectAnim = GameObject.FindGameObjectWithTag("FadeObject").GetComponent<Animation>();
        deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        deathMenu.SetActive(false);
        SceneManager.sceneLoaded += FindObjectsOnSceneLoad;
        _timer.OnTimerStopped += Explode;
    }
    private void FindObjectsOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        fadeObjectAnim = GameObject.FindGameObjectWithTag("FadeObject").GetComponent<Animation>();
        deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        deathMenu.SetActive(false);
    }
    private void Explode()
    {
        _audioSource.Play();
        fadeObjectAnim.Play();
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
