using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Light), (typeof(AudioSource)))]

public class FlickeringLight : MonoBehaviour
{
    [Header("Set the flicer intensity")]
    [SerializeField] private float currentValue = 0;
    [SerializeField] private float minValue = 5;
    [SerializeField] private float maxValue = 35;
    [SerializeField] private GameObject lightBulb = null;
    [SerializeField] private GameObject lightSwitch = null;

    [Header("Set the flicer time")]
    [SerializeField] private float minTime = 0.1f;
    [SerializeField] private float maxTime = 1;

    private Light _light => GetComponent<Light>();
    private Renderer Render => lightBulb.GetComponent<Renderer>();
    private ColourLights LightsScript => lightSwitch.GetComponent<ColourLights>();

    private void FixedUpdate()
    {
        if (LightsScript.currentColor != 0)
        {
            _light.intensity = currentValue;
            StartCoroutine(Flicker());

            if (_light.intensity <= 10)
            {
                Render.material.SetColor("_EmissionColor", LightsScript.colors[0]);
            }

            else
            {
                Render.material.SetColor("_EmissionColor", LightsScript.colors[LightsScript.currentColor]);
            }
        }

        else
        {
            return;
        }
    }

    private IEnumerator Flicker()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        currentValue = Random.Range(minValue, maxValue);
    }
}
