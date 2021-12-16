using UnityEngine;
using UnityEngine.UI;

public class InteractiveObjects : MonoBehaviour, IInteractive
{
    [SerializeField]
    protected enum objectOptions
    {
        Triangle,
        Square,
        Circle,
        Mirror,
        Key
    }

    [Header("Required Object to pass")]
    [SerializeField]
    protected objectOptions requiredObject;

    [Header("Detect Player")]
    [SerializeField]
    protected GameObject player;
    protected ViewManager viewManager;
    protected float detectionR = 50;
    protected Collider[] colliders;

    [Header("Get Player Inventory")]
    protected PlayerInventory inventory;
    protected Collectable collected;

    [Tooltip("Fill fields if object has a dialogue reaction")]
    [Header("Dialogue Text")]
    [SerializeField]
    protected Text textArea = null;
    protected IDialogue DialogueTxt = null;
    public bool isAvailable { get; set; }

    protected string _requiredObject;

    protected void Start()
    {
        viewManager = FindObjectOfType<ViewManager>();
        _requiredObject = requiredObject.ToString();

        if (player == null)
        {
            player = FindObjectOfType<PlayerController1>().gameObject;
        }

        inventory = player.gameObject.GetComponent<PlayerInventory>();
        collected = gameObject.GetComponent<Collectable>();

        if (GetComponent<IDialogue>() != null)
        {
            DialogueTxt = GetComponent<IDialogue>();
        }
    }

    protected void SetTextArea(string text)
    {
        textArea.text = text;
    }
}
