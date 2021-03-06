using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    private FlickeringLight[] Lights;
    private MenuMusic MusicFade;

    private bool newGame = false;
    private bool loadGame = false;
    private bool toMainMenu = false;

    void Awake()
    {
        MusicFade = GetComponent<MenuMusic>();
        Lights = FindObjectsOfType<FlickeringLight>();

        newGame = false;
        loadGame = false;
        toMainMenu = false;

        if (PlayerPrefs.GetInt("SoundSettings") == 1)
        {
            AudioListener.volume = PlayerPrefs.GetFloat("VolumeSetting");
        }

        else
        {
            AudioListener.volume = 0;
        }

        if (PlayerPrefs.GetInt("LightSettings") == 1)
        {
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i].enabled = true;
            }
        }

        else
        {
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i].enabled = false;
            }
        }
    }

    private void Update()
    {
        if (newGame)
        {
            StartCoroutine(StartNewSave());
        }

        if (loadGame)
        {
            StartCoroutine(LoadSave());
        }

        if (toMainMenu)
        {
            StartCoroutine(BackToMainMenu());
        }
    }

    public void NewGame()
    {
        newGame = true;
    }

    public void ContinueLevel()
    {
        loadGame = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        toMainMenu = true;
    }

    public void InGameBackToMenu()
    {
        SceneManager.LoadScene("MainMenuLvl");
    }

    public void GameWon()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void GameLost()
    {
        SceneManager.LoadScene("LostScene");
    }

    private IEnumerator StartNewSave()
    {
        MusicFade.FadeMusic();

        yield return new WaitForSeconds(1f);

        PlayerPrefs.SetInt("FirstRoomCompleted", 0);
        PlayerPrefs.SetInt("SecondRoomCompleted", 0);
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadSave()
    {
        MusicFade.FadeMusic();

        yield return new WaitForSeconds(1f);

        PlayerPrefs.GetInt("FirstRoomCompleted");
        PlayerPrefs.GetInt("SecondRoomCompleted");
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator BackToMainMenu()
    {
        MusicFade.FadeMusic();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenuLvl");
    }
}
