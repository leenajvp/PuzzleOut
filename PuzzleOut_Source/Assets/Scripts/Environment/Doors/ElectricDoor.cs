using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource), typeof(BoxCollider))]

public class ElectricDoor : MonoBehaviour, IInteractive, IActivator
{
    public bool isUnlocked;
    public bool open { get; set; }
    public bool isAvailable { get; set; }
    public bool isPassed { get; set; }

    [Header("Lock lights")]
    [SerializeField] private GameObject lockLight = null;
    public Color opened;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource doorSlide;
    [SerializeField] private AudioSource locked;

    [Header("Dialogue Text")]
    [Tooltip("Fill fields if object has a dialogue reaction")]
    [Header("Dialogue Text")]
    [SerializeField] protected Text textArea = null;
    protected IDialogue DialogueTxt = null;

    [Header("Open Door")]
    [SerializeField] private float doorOpenAngle = 55.0f;
    [SerializeField] private float speed = 0.01f;
    private float defaultRotationAngle;
    private float currentRotationAngle;
    private float openTime = 0;

    void Start()
    {
        if (doorSlide == null)
        {
            doorSlide = GetComponent<AudioSource>();
        }


        if (GetComponent<IDialogue>() != null)
        {
            DialogueTxt = GetComponent<IDialogue>();
        }

        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        isPassed = false;
    }

    void Update()
    {
        if (isUnlocked == true)
        {
            OpenLock();
        }

        if (isUnlocked == true && open == true)
        {
            OpenDoor();
        }

        if (isUnlocked == true && open == false)
        {
            OpenDoor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            OpenDoor();
        }
    }

    private void OnMouseDown()
    {
        if (isUnlocked == true)
        {
            doorSlide.Play();
        }

        if (isUnlocked == true && open == false)
        {
            open = true;

        }

        else
        {
            open = false;
        }

        if (isUnlocked == false)
        {
            locked.Play();
            SetTextArea(DialogueTxt.GetDialogue());
        }

    }

    public void OpenLock()
    {
        isPassed = true;
        lockLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", opened);
        isAvailable = true;
        isUnlocked = true;

    }

    private void OpenDoor()
    {
        openTime += Time.deltaTime * speed;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
    }

    protected void SetTextArea(string text)
    {
        textArea.text = text;
    }
}
