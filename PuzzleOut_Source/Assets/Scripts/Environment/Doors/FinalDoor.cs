using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource), typeof(BoxCollider), typeof(IDialogue))]

public class FinalDoor : InteractiveObjects, IActivator
{
    [Header("Open Door")]
    public bool unlocked;
    public bool open;
    public bool isPassed { get; set; }
    [SerializeField] private float doorOpenAngle = 55.0f;
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float defaultRotationAngle;
    [SerializeField] private float currentRotationAngle;
    [SerializeField] private float openTime = 0;
    private bool entering;

    [SerializeField] private GameObject receivedKey = null;
    private Animator Anim => GetComponent<Animator>();

    [Header("Sound Effects")]
    [SerializeField] private AudioSource doorSlide = null;
    [SerializeField] private AudioSource keySound = null;
    [SerializeField] private AudioSource needKey = null;

    private new void Start()
    {
        base.Start();

        receivedKey.SetActive(false);
        isAvailable = true;

        isPassed = false;
        unlocked = false;
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    private void Update()
    {
        if (unlocked == true)
        {
            receivedKey.transform.Rotate(0, 1, 0 * Time.deltaTime);
            isAvailable = false;
        }

        if (open == true)
        {
            OpenDoor();
        }

        if (unlocked == true && open == true)
        {
            OpenDoor();
        }

        if (unlocked == true && open == false)
        {
            OpenDoor();
        }
    }

    private void CheckIfItemCorrect()
    {
        if (inventory != null)
        {
            for (int i = 0; i < inventory.CollectedObjects.Count; i++)
            {
                if (inventory.CollectedObjects[i].name == _requiredObject)
                {
                    Anim.SetTrigger("play");
                    StartCoroutine(InsertKey());
                    isPassed = true;
                }

                else
                {
                    needKey.PlayDelayed(1);
                    SetTextArea(DialogueTxt.GetDialogue());
                }
            }
        }

        if (inventory.CollectedObjects.Count == 0 && isPassed == false)
        {
            needKey.Play();
            SetTextArea(DialogueTxt.GetDialogue());
        }
    }

    private void OnMouseDown()
    {
        if (unlocked)
        {
            doorSlide.Play();
        }

        if (unlocked == false && open == false)
        {
            if (entering == false)
            {
                entering = true;

                Anim.SetTrigger("play");
                CheckIfItemCorrect();
            }

            else
            {
                entering = false;
            }
        }

        if (unlocked == true && open == false)
        {
            open = true;
        }

        else
        {
            open = false;
        }
    }

    private IEnumerator InsertKey()
    {
        receivedKey.SetActive(true);
        keySound.Play();

        for (int i = 0; i < inventory.CollectedObjects.Count; i++)
        {
            var correctObject = inventory.CollectedObjects[i].gameObject;
            Destroy(correctObject);
            inventory.CollectedObjects.Clear();
        }

        yield return new WaitForSeconds(2);

        unlocked = true;
        receivedKey.SetActive(false);
        ViewMngr.camIsForced = true;
        open = true;
        doorSlide.Play();
    }

    private void OpenDoor()
    {
        openTime += Time.deltaTime * speed;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
    }
}
