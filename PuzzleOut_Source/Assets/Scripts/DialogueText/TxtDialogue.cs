using System.Collections;
using UnityEngine;

public class TxtDialogue : MonoBehaviour, IDialogue
{

    [SerializeField]
    protected enum TriggerMethod
    {
        Raycast,
        MouseClickOrOther
    }

    [Tooltip("How is this dialogue triggered")]
    [SerializeField] private TriggerMethod triggerMethod = TriggerMethod.Raycast;

    [Tooltip("Required to work")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] private float dialogueTime = 5f;
    public string dialogue = "";
    public bool dialogueAvailable { get; set; }

    private void Start()
    {

        if (triggerMethod == TriggerMethod.Raycast)
        {
            dialogueAvailable = true;
        }

        else
        {
            dialogueAvailable = false; ;
        }
    }

    public string GetDialogue()
    {
        StartCoroutine(DialogueTimer());
        dialoguePanel.SetActive(true);
        string result = dialogue;
        dialogueAvailable = false;
        return result;
    }

    private IEnumerator DialogueTimer()
    {
        yield return new WaitForSeconds(dialogueTime);

        dialoguePanel.SetActive(false);
    }
}
