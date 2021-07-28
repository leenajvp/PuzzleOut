using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [Header("Player Controls Settings")]
    public Dropdown inputOptions;
    private static int value;

    [Header("Player Controls Settings")]
    public Dropdown difficulty;
    private static int dvalue;

    [Header("Audio Settings")]
    [SerializeField] private Toggle sounds = null;
    [Tooltip("AudioListener in the scene must be added")]
    [SerializeField] private AudioListener gameSounds = null;

    [Header("Remove Flickering Lights")]
    [SerializeField] private Toggle flashingLights = null;

    [Header("Colorblind Mode")]
    [SerializeField] private Toggle easyMode = null;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayerControls") == 1)
        {
            inputOptions.GetComponent<Dropdown>().value = 1;
        }

        else
        {
            inputOptions.GetComponent<Dropdown>().value = 0;
        }


        if (PlayerPrefs.GetInt("Difficulty") == 0)
        {
            difficulty.GetComponent<Dropdown>().value = 0;
        }

        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            difficulty.GetComponent<Dropdown>().value = 1;
        }

        if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            difficulty.GetComponent<Dropdown>().value = 2;
        }

        if (PlayerPrefs.GetInt("ColorBlind") == 1)
        {
            easyMode.isOn = true;
        }

        else
        {
            easyMode.isOn = false;
        }

        if (PlayerPrefs.GetInt("SoundSettings") == 1)
        {
            sounds.isOn = true;
        }

        else
        {
            sounds.isOn = false;
        }

        if (PlayerPrefs.GetInt("LightSettings") == 1)
        {
            flashingLights.isOn = true;
        }

        else
        {
            flashingLights.isOn = false;
        }

    }

    private void Update()
    {
        if (gameSounds != null)
        {
            GameSoundsInCurrentScene();
        }

        DifficultySettings();
        PlayerControls();
        ManageLights();
        ManageAudio();
        EasyMode();
    }

    public void PlayerControls()
    {
        value = inputOptions.GetComponent<Dropdown>().value;

        if (value == 0)
        {
            PlayerPrefs.SetInt("PlayerControls", 0);
        }

        if (value == 1)
        {
            PlayerPrefs.SetInt("PlayerControls", 1);
        }
    }

    private void DifficultySettings()
    {
        dvalue = difficulty.GetComponent<Dropdown>().value;

        if (dvalue == 0)
        {

            PlayerPrefs.SetInt("Difficulty", 0);
        }

        if (dvalue == 1)
        {

            PlayerPrefs.SetInt("Difficulty", 1);
        }

        if (dvalue == 2)
        {

            PlayerPrefs.SetInt("Difficulty", 2);
        }
    }

    private void ManageAudio()
    {
        if (sounds.isOn == true)
        {
            PlayerPrefs.SetInt("SoundSettings", 1);
        }

        else
        {
            PlayerPrefs.SetInt("SoundSettings", 0);
        }
    }

    private void ManageLights()
    {
        if (flashingLights.isOn == true)
        {
            PlayerPrefs.SetInt("LightSettings", 1);
        }

        else
        {
            PlayerPrefs.SetInt("LightSettings", 0);
        }
    }

    private void EasyMode()
    {
        if (easyMode.isOn == true)
        {
            PlayerPrefs.SetInt("ColorBlind", 1);
        }

        else
        {
            PlayerPrefs.SetInt("ColorBlind", 0);
        }
    }

    private void GameSoundsInCurrentScene()
    {
        if (PlayerPrefs.GetInt("SoundSettings") == 1)
        {
            gameSounds.enabled = true;
        }

        else
        {
            gameSounds.enabled = false;
        }
    }
}
