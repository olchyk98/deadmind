using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private TimerUtil _timer;
    [SerializeField]
    private Animation fadeObjectAnim;
    [SerializeField]
    private GameObject deathMenu;
    private AudioSource _audioSource;
    private void Start()
    {
        _timer = GetComponent<TimerUtil>();
        _audioSource = GetComponent<AudioSource>();
        fadeObjectAnim = GameObject.FindGameObjectWithTag("FadeObject").GetComponent<Animation>();
        deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        deathMenu.SetActive(false);
        _timer.OnTimerStopped += Explode;
    }
    private void Explode()
    {
        _audioSource.Play();
        fadeObjectAnim.Play();
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Destroy(_timer.gameObject,3);
    }
}
