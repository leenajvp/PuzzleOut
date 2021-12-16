using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonPuzzle : MonoBehaviour, IInteractive
{
    public bool isAvailable { get; set; }

    [Header ("Enter Correct code")]
    [SerializeField] 
    private Camera objectCamera = null;
    [SerializeField] 
    private KeyCode[] correctCode = null;
    private int enteredCode = 0;

    [Header("Door to open")]
    [SerializeField] 
    private GameObject door = null;
    private ElectricDoor Edoor;

    [Header ("Scale buttons on click")]
    [SerializeField] 
    private GameObject[] buttons = null;
    [SerializeField]
    private Vector3 defaultScale = new Vector3 (0.2f,0.3f,0.6f);
    [SerializeField]
    private Vector3 pressedScale = new Vector3 (0.2f,0.4f,0.6f);

    [Header("Sound Effects")]
    [SerializeField] 
    private AudioSource ButtonPress = null;
    [SerializeField] 
    private AudioSource lockOpen = null;
    
    [Header ("UI Elements")]
    [SerializeField] 
    private GameObject buttonUI = null;
    [SerializeField] 
    private GameObject savingImage = null;

    protected ViewManager ObjectManager; 

    private void Start()
    {
        Edoor = door.GetComponent<ElectricDoor>();
        ObjectManager = FindObjectOfType<ViewManager>();

        savingImage.SetActive(false);
        

        if (PlayerPrefs.GetInt("FirstRoomCompleted") == 1)
        {
            isAvailable = false;
            buttonUI.SetActive(false);
            Edoor.isUnlocked = true;
            Edoor.isAvailable = true;
            gameObject.layer = 0;
            lockOpen.Play();
        }

        else
        {
            isAvailable = true;
            buttonUI.SetActive(false);
            Edoor.isUnlocked = false;
            Edoor.isAvailable = false;
            gameObject.layer = 12;
        }

    }

    private void Update() 
    {
        ActivatePuzzle();
    }

    private void ActivatePuzzle()
    {
        if (isAvailable && objectCamera.enabled == true)
        {
            buttonUI.SetActive(true);
            ReceiveCode();
            PressButton();
        }

        else if (isAvailable && objectCamera.enabled == false)
        {
            buttonUI.SetActive(false);
            ReturnButtons();
        }

        else
        {
            buttonUI.SetActive(false);
        }
    }

    private void CorrectCodeEntered()
    {
        Edoor.isUnlocked = true;
        lockOpen.Play();
        ObjectManager.camIsForced = true;
        
        isAvailable = false;
        
        PlayerPrefs.SetInt("FirstRoomCompleted", 1);

        StartCoroutine(Saving());
    }

    private void ReceiveCode()
    {
        if (isAvailable == true && Input.GetKeyDown(correctCode[enteredCode]))
        {
           
            if (++enteredCode == correctCode.Length)
            {
                CorrectCodeEntered();
            }
        }
        else if (isAvailable == true && Input.anyKeyDown)
        {
            enteredCode = 0;
        }
    }

    // Scale buttons based on keyboard inputs
    private void PressButton()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ButtonPress.Play();
                buttons[0].gameObject.transform.localScale = pressedScale;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ButtonPress.Play();
                buttons[1].gameObject.transform.localScale = pressedScale;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ButtonPress.Play();
                buttons[2].gameObject.transform.localScale = pressedScale;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ButtonPress.Play();
                buttons[3].gameObject.transform.localScale = pressedScale;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ButtonPress.Play();
                buttons[4].gameObject.transform.localScale = pressedScale;
            }

            if (enteredCode == 0 && buttons[0].gameObject.transform.localScale == pressedScale && buttons[1].gameObject.transform.localScale == pressedScale && buttons[2].gameObject.transform.localScale == pressedScale && buttons[3].gameObject.transform.localScale == pressedScale && buttons[4].gameObject.transform.localScale == pressedScale)
            {
                ReturnButtons();
            }
        }
    }

    //Set buttons to original scale
    private void ReturnButtons()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.transform.localScale = defaultScale;
            enteredCode = 0;
        }
    }

    private IEnumerator Saving()
    {
        savingImage.SetActive(true);

        yield return new WaitForSeconds(5);

        savingImage.SetActive(false);
    }
}

