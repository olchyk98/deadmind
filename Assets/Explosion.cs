using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private TimerUtil _timer;
    private AudioSource _audioSource;
    private void Start()
    {
        _timer = GetComponent<TimerUtil>();
        _audioSource = GetComponent<AudioSource>();
        _timer.OnTimerStopped += Explode;
    }
    private void Explode()
    {
        var fadeObjectAnim = GameObject.FindGameObjectWithTag("FadeObject").GetComponent<Animation>();
        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject gameObject in allObjects)
        {
            if(gameObject.CompareTag("DeathMenu"))
            {
                gameObject.SetActive(true);
            }
        }
        _audioSource.Play();
        fadeObjectAnim.Play();
        Cursor.lockState = CursorLockMode.None;
        Destroy(_timer.gameObject,3);
    }
}
