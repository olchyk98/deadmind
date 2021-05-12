using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintingManager : Interactable
{
    public Painting selectedPainting;
    public UnityEvent CompleteEvent;
    System.Random rNG = new System.Random();
    private Painting[] _paintings;
    public List<Material> fakePaintings;
    public Material realPainting;
    private int correctPaintingIndex;
    private void Start()
    {
        OnInteract += GuessPainting;
        _paintings = GetComponentsInChildren<Painting>();
        foreach (Painting painting in _paintings)
        {
            painting.paintingRenderer.material = fakePaintings[rNG.Next(0, fakePaintings.Count)];
        }
        correctPaintingIndex = rNG.Next(0, _paintings.Length);
        _paintings[correctPaintingIndex].paintingRenderer.material = realPainting;
    }
    private void GuessPainting()
    {
        if(selectedPainting == _paintings[correctPaintingIndex])
        {
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
            selectedPainting.light.color = Color.red;
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
