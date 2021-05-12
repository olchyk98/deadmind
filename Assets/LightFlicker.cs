using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light[] _lights;
    [SerializeField]
    private int _curNumber = 5;
    void Start()
    {
        _lights = GetComponentsInChildren<Light>();
    }
    void Update()
    {
        _curNumber += Random.Range(-1, 2);
        _curNumber = Mathf.Clamp(_curNumber, 0, 10);

        foreach (Light light in _lights)
        {
            light.enabled = (_curNumber >= 5);
        }
    }
}
