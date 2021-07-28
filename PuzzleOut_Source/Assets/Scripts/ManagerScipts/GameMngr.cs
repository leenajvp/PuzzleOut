using System.Collections;
using UnityEngine;

public class GameMngr : PlayerDeactivator
{
    [Header("Game Intro")]
    [Header("FlashLight")]
    [SerializeField] private Light[] playerLights = null;
    [SerializeField] private AudioSource lightOn = null;
    [SerializeField] private AudioSource lightOff = null;
    [Header("PlayerSounds")]
    [SerializeField] private AudioSource riseUpSeq = null;
    [SerializeField] private AudioSource deafultBreathing = null;
    [Header("UI elements")]
    [SerializeField] private GameObject mousepointer = null;
    [SerializeField] private GameObject pauseUI = null;
    [SerializeField] private GameObject pointerUI = null;
    [SerializeField] private GameObject dialoguePanel = null;

    [Header("UI Actions")]
    public bool canPause;
    public bool itemCollected;
    public bool lockedCamera;
    [SerializeField] private GameObject pausedMenu = null;

    [Header("To remove unwanted objects")]
    [Header("After Level 1")]
    [SerializeField] private GameObject[] firstLevelObjects = null;

    [Header("After Level 2")]
    [SerializeField] private GameObject[] secondLevelObjects = null;

    void Start()
    {

        if (mousepointer == null)
        {
            mousepointer = GameObject.FindGameObjectWithTag("MousePointer");
        }

        if (Cursor.visible == true)
        {
            Cursor.visible = false;
        }

        canPause = true;
        itemCollected = false;
        lockedCamera = false;
        pausedMenu.SetActive(false);

        if (PlayerPrefs.GetInt("First") == 1)
        {
            for (int i = 0; i < firstLevelObjects.Length; i++)
            {
                firstLevelObjects[i].SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("Second") == 1)
        {
            for (int i = 0; i < secondLevelObjects.Length; i++)
            {
                secondLevelObjects[i].SetActive(false);
            }
        }

        if ((PlayerPrefs.GetInt("First") == 0) && (PlayerPrefs.GetInt("Second") == 0))
        {
            for (int i = 0; i < playerLights.Length; i++)
            {
                playerLights[i].enabled = false;
            }

            StartCoroutine(GameIntro());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause == true)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pausedMenu != null)
        {
            PlayerDisable();
            pausedMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

    public void ReturnGame()
    {
        pausedMenu.SetActive(false);
        Time.timeScale = 1;
        PlayerReturn();
        Cursor.visible = false;
    }

    private IEnumerator GameIntro()
    {
        pauseUI.SetActive(false);
        canPause = false;
        PlayerDisable();
        riseUpSeq.Play();
        Cursor.visible = false;
        mousepointer.SetActive(false);

        yield return new WaitForSeconds(15);

        for (int i = 0; i < playerLights.Length; i++)
        {
            lightOn.Play();
            playerLights[i].enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < playerLights.Length; i++)
        {
            lightOff.Play();
            playerLights[i].enabled = false;
        }

        PlayerReturn();
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < playerLights.Length; i++)
        {
            lightOn.Play();
            playerLights[i].enabled = true;
        }

        pauseUI.SetActive(true);
        pointerUI.SetActive(true);
        deafultBreathing.Play();
        dialoguePanel.SetActive(false);
        canPause = true;
        mousepointer.SetActive(true);
    }
}
