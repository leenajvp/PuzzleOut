using UnityEngine;

public class ColourLights : MonoBehaviour, IInteractive
{
    [Header("Colour Lights")]
    public Color[] colors = new Color[3];
    public Light adjustableLight;
    public GameObject lightBulb;
    public int currentColor, length;
    private Renderer Render => lightBulb.GetComponent<Renderer>();

    [Header("Colour to pass")]
    public int correctColour;
    public bool completed;
    public bool isAvailable { get; set; }

    [Header("Colour Blind / Extra Cues")]
    [SerializeField] private GameObject cue = null;

    private AudioSource flickerSound => adjustableLight.GetComponent<AudioSource>();

    void Start()
    {
        cue.SetActive(false);

        if (PlayerPrefs.GetInt("Second") == 1)
        {
            isAvailable = false;
            adjustableLight.color = colors[correctColour];
            currentColor = correctColour;
            Render.material.SetColor("_EmissionColor", colors[correctColour]);
        }

        else
        {
            isAvailable = true;
            completed = false;
            currentColor = 0;
            length = colors.Length;
            adjustableLight.color = colors[currentColor];
            Render.material.SetColor("_EmissionColor", colors[currentColor]);
        }
    }

    private void Update()
    {
        SoundEffects();

        if (PlayerPrefs.GetInt("ColorBlind") == 1 || PlayerPrefs.GetInt("Difficulty") == 0)
        {
            EasyMode();
        }

        if (currentColor == correctColour)
        {
            completed = true;
        }

        else
        {
            completed = false;
        }
    }

    private void OnMouseDown()
    {
        if (isAvailable)
        {
            currentColor = (currentColor + 1) % length;
            adjustableLight.color = colors[currentColor];
            Render.material.SetColor("_EmissionColor", colors[currentColor]);
        }
    }

    private void EasyMode()
    {
        if (currentColor == correctColour)
        {
            cue.SetActive(true);
        }

        else
        {
            cue.SetActive(false);
        }
    }

    private void SoundEffects()
    {
        if (currentColor != 0 && flickerSound.isPlaying == false)
        {
            flickerSound.Play();
        }

        if (currentColor == 0 && flickerSound.isPlaying == true)
        {
            flickerSound.Stop();
        }
    }
}
