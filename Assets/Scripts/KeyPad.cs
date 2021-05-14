using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeyPad : MonoBehaviour
{
    [SerializeField]
    int combinationLength;
    [SerializeField]
    string correctCombination;
    [SerializeField]
    string cipheredCombination;

    [SerializeField]
    char[] cipherSymbols = new char[10];
    public string inputCombination = "";

    public UnityEvent unlockEvent;
    [SerializeField]
    TextMeshProUGUI displayText;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] outcomeSounds;
    private bool _isWaiting;

    private void Start()
    {
        foreach (var v in gameObject.GetComponentsInChildren<KeyPadButton>())
        {
            v.OnKeyPress += EnterNumber;
        }
        for (int i = 0; i < combinationLength; i++)
        {
            correctCombination += Random.Range(0, 9);
        }
        cipheredCombination = CodeToCipher(correctCombination);
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (inputCombination != correctCombination) displayText.text = inputCombination;

        if (inputCombination == correctCombination)
        {
            displayText.text = "OK";
            _audioSource.clip = outcomeSounds[0];
            _audioSource.Play();
            unlockEvent.Invoke();
            enabled = false;
        }
        else if(string.IsNullOrEmpty(inputCombination))
        {
            displayText.text = cipheredCombination;
        }
        else if (inputCombination.Length >= combinationLength)
        {
            displayText.text = "WRONG";
            if(!_isWaiting) StartCoroutine(clearAfterSeconds(3));
            _isWaiting = true;
        }
    }

    public void EnterNumber(int number)
    {
        inputCombination += number;
    }

    private string CodeToCipher(string originalString)
    {
        string cipheredString = "";

        foreach (char number in originalString)
        {
            cipheredString += cipherSymbols[number-48];
        }
        return cipheredString;
    }

    private IEnumerator clearAfterSeconds(float seconds)
    {
        _audioSource.clip = outcomeSounds[1];
        _audioSource.Play();
        yield return new WaitForSecondsRealtime(seconds);
        inputCombination = "";
        _isWaiting = false;
    }
}
