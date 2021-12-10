using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class AudioTrigger : MonoBehaviour
{
    private bool isAvailable = true;

    [Tooltip("Fill these fields if the trigger has a dialogue reaction")]
    [Header("Dialogue Text")]
    [SerializeField] 
    private Text textArea = null;
    private IDialogue DialogueTxt = null;

    private void Start()
    {
        isAvailable = true;

        if (GetComponent<IDialogue>() != null)
        {
            DialogueTxt = GetComponent<IDialogue>();
        }
    }

    void SetTextArea(string text)
    {
        textArea.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isAvailable)
        {
            GetComponent<AudioSource>().Play();
            isAvailable = false;

            if (DialogueTxt != null)
            {
                SetTextArea(DialogueTxt.GetDialogue());
            }

            else
            {
                return;
            }
        }
    }
}
