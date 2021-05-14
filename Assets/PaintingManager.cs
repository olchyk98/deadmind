using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PaintingManager : Interactable
{
    public Painting selectedPainting;
    public UnityEvent CompleteEvent;
    System.Random rNG = new System.Random();
    [SerializeField]
    private Painting[] _paintings;
    public List<Material> fakePaintings;

    private TextMeshProUGUI ownedByText;
    public List<Material> realPaintings;
    [SerializeField]
    private List<string> realPaintingOwners;

    private int correctPaintingIndex;
    private Animation _animation;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip buttonPressSound;
    [SerializeField]
    private AudioClip[] outcomeSounds;

    private void Start()
    {
        OnInteract += GuessPainting;
        _paintings = GetComponentsInChildren<Painting>();
        ownedByText = GetComponentInChildren<TextMeshProUGUI>();
        foreach (Painting painting in _paintings)
        {
            painting.paintingRenderer.material = fakePaintings[rNG.Next(0, fakePaintings.Count)];
        }
        correctPaintingIndex = rNG.Next(0, _paintings.Length);

        int randomPaintingIndex = rNG.Next(realPaintings.Count);

        _paintings[correctPaintingIndex].paintingRenderer.material = realPaintings[randomPaintingIndex];
        ownedByText.text = realPaintingOwners[randomPaintingIndex];
        _animation = GetComponentInChildren<Animation>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    private void GuessPainting()
    {
        if (selectedPainting == null) return;

        _animation.Play();
        _audioSource.clip = buttonPressSound;
        _audioSource.Play();

        if (selectedPainting == _paintings[correctPaintingIndex])
        {
            _audioSource.clip = outcomeSounds[0];
            _audioSource.Play();
            selectedPainting.light.color = Color.green;
            CompleteEvent.Invoke();
            CanInteract = false;
            foreach (Painting painting in _paintings)
            {
                painting.CanInteract = false;
            }
        }
        else
        {
            _audioSource.clip = outcomeSounds[1];
            _audioSource.Play();
            selectedPainting.light.color= Color.red;
            StartCoroutine(wrongPaintingCooldown());
        }
    }
    private IEnumerator wrongPaintingCooldown()
    {
        foreach (Painting painting in _paintings)
        {
            painting.CanInteract = false;
        }
        CanInteract = false;
        yield return new WaitForSeconds(4);
        CanInteract = true;
        selectedPainting.light.color = Color.white;
        foreach (Painting painting in _paintings)
        {
            painting.CanInteract = true;
        }
    }
}
