using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Interactable
{
    private PaintingManager paintingManager;
    public Light light;
    public MeshRenderer paintingRenderer;
    private void Start()
    {
        paintingRenderer = GetComponentInChildren<MeshRenderer>();
        light = GetComponentInChildren<Light>();
        paintingManager = GetComponentInParent<PaintingManager>();
        OnInteract += SetSelected;
    }
    private void Update()
    {
        light.enabled = object.ReferenceEquals(paintingManager.selectedPainting, this);
    }
    private void SetSelected()
    {
        paintingManager.selectedPainting = this;
    }
}
