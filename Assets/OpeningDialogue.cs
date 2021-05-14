using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class OpeningDialogue : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private float waitBeforeStart;
    [SerializeField]
    private float delayBetweenClips;
    [SerializeField]
    private List<AudioClip> dialogueAudio;
    [SerializeField]
    private List<string> dialogueSubtitles;
    [SerializeField]
    private TextMeshProUGUI subtitleText;
    [SerializeField]
    private UnityEvent dialogueFinishedEvent;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(playDialogue());
    }
    IEnumerator playDialogue()
    {
        yield return new WaitForSeconds(waitBeforeStart);
        for (int i = 0; i < dialogueAudio.Count; i++)
        {
            subtitleText.text = dialogueSubtitles[i];
            _audioSource.PlayOneShot(dialogueAudio[i],1);
            yield return new WaitForSeconds(dialogueAudio[i].length);
            subtitleText.text = "";
            yield return new WaitForSeconds(delayBetweenClips);
        }
        dialogueFinishedEvent.Invoke();
        subtitleText.enabled = false;
        this.enabled = false;
    }
}
