using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.HighDefinition;

public class Button3D : Interactable
{
    private float _initialLightIntensity = 0f;
    public UnityAction OnPress;
    private HDAdditionalLightData _light;
    private Animation _animation;
    private void Start()
    {
        _animation = GetComponent<Animation>();
        _light = GetComponent<Light>().GetComponent<HDAdditionalLightData>();

        if(_light != null) {
            _initialLightIntensity = _light.intensity;
            _light.intensity = 0f;
        }
    }

    private void OnMouseDown()
    {
        OnPress?.Invoke();
        if(_animation != null) _animation.Play();
    }

    public IEnumerator Blink ()
    {
        if(_light == null)
            yield break;

        _light.intensity = _initialLightIntensity;

        yield return new WaitForSeconds(.4f);

        _light.intensity = 0f;
    }
}
