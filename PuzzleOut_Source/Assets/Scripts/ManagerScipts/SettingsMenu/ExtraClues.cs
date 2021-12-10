using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExtraClues : MonoBehaviour
{
    [SerializeField]
    protected enum ClueLevel
    {
        First,
        Second,
        Third
    }
    [Tooltip("If the clues level is passed, remove clue")]
    [SerializeField] 
    private ClueLevel clueLevel = ClueLevel.First;

    [Tooltip("Guide player how to trigger clue")]
    [SerializeField] 
    private GameObject extraClueUI = null;
    [SerializeField] 
    private GameObject extraCluePanel = null;
    [SerializeField] 
    private AudioSource extraClue = null;
    [Tooltip("After triggering, how long to activate this clue")]
    [SerializeField] 
    private float triggerTime = 300f;
    [Tooltip("Reading Time for clue")]
    [SerializeField] 
    private float removeTIme = 5f;

    public float existingTime = 0f;
    private bool isActivated = false;
    private bool isAvailable = false;

    [Header("Dialogue Text")]
    [SerializeField] 
    private string dialogue = "";
    [SerializeField] 
    private Text textArea = null;

    void Start()
    {
        isActivated = false;
        gameObject.SetActive(false);
        extraClueUI.SetActive(false);
        isAvailable = true;
        existingTime = 0;
    }

    void Update()
    {
        if (clueLevel == ClueLevel.First)
        {
            if (PlayerPrefs.GetInt("First") == 1)
            {
                isAvailable = false;
                extraClueUI.SetActive(false);
            }
        }

        if (clueLevel == ClueLevel.Second)
        {
            if (PlayerPrefs.GetInt("Second") == 1)
            {
                isAvailable = false;
                extraClueUI.SetActive(false);
            }
        }

        if (isActivated == false)
        {
            existingTime += Time.deltaTime;
        }


        if (existingTime >= triggerTime && isAvailable == true)
        {
            extraClueUI.SetActive(true);
            Debug.Log("First");

            if (Input.GetKeyDown(KeyCode.E))
            {
                isActivated = true;
                existingTime = 0;
                extraClueUI.SetActive(false);
                extraCluePanel.SetActive(true);
                SetTextArea(GetDialogue());
                extraClue.Play();
                StartCoroutine(RemovingTimer());
            }
        }
    }

    public string GetDialogue()
    {
        string result = dialogue;
        return result;
    }

    private void SetTextArea(string text)
    {
        textArea.text = text;
    }

    private IEnumerator RemovingTimer()
    {
        yield return new WaitForSeconds(removeTIme);

        gameObject.SetActive(false);
        extraCluePanel.SetActive(false);
    }
}
