using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource), typeof(BoxCollider), typeof(IDialogue))]

[System.Serializable]
public class ShapeDoors : InteractiveObjects, IActivator
{
    [Header("Slide Door Open")]
    [SerializeField] private float openDistance = 1f;
    [SerializeField] private float openSpeed = 2f;
    [Tooltip("Object placed in the slot")]
    [SerializeField] protected GameObject receivedObject;
    public bool open = false;

    public bool isPassed { get; set; }

    private Vector3 defaultPos;
    private Transform Door => gameObject.transform;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource doorSlide;
    [SerializeField] private AudioSource placingSound = null;
    [SerializeField] private AudioSource doesntFit = null;
    private bool entering;

    private new void Start()
    {
        entering = false;
        base.Start();
        player = FindObjectOfType<PlayerController1>().gameObject;
        isPassed = false;
        isAvailable = true;
        defaultPos = gameObject.transform.position;


        if (doorSlide == null)
        {
            doorSlide = GetComponent<AudioSource>();
        }

        if (PlayerPrefs.GetInt("Second") == 1)
        {
            open = true;
            isAvailable = false;
        }

        else
        {
            receivedObject.SetActive(false);
            isAvailable = true;
        }
    }

    public void Update()
    {
        Door.position = new Vector3(Door.position.x, Door.position.y, Mathf.Lerp(Door.position.z, defaultPos.z + (open ? openDistance : 0), Time.deltaTime * openSpeed));
    }



    public void OnMouseDown()
    {

        if (entering == false)
        {
            entering = true;
            CheckIfItemCorrect();
        }

        else
        {
            entering = false;
        }
    }

    private void CheckIfItemCorrect()
    {
        if (entering == true)
        {
            if (inventory != null)
            {
                for (int i = 0; i < inventory.CollectedObjects.Count; i++)
                {
                    if (inventory.CollectedObjects[i].name == _requiredObject && inventory)
                    {
                        StartCoroutine(OpenDoor());
                    }

                    else
                    {
                        doesntFit.Play();
                        SetTextArea(DialogueTxt.GetDialogue());
                    }
                }
            }
        }

    }

    private IEnumerator OpenDoor()
    {
        receivedObject.SetActive(true);

        if (placingSound != null)
        {
            placingSound.Play();
        }

        for (int i = 0; i < inventory.CollectedObjects.Count; i++)
        {
            var correctObject = inventory.CollectedObjects[i].gameObject;
            correctObject.SetActive(false);
            inventory.CollectedObjects.Clear();
        }

        yield return new WaitForSeconds(2);

        ViewMngr.camIsForced = true;

        yield return new WaitForSeconds(1);

        doorSlide.Play();
        open = true;
        isAvailable = false;
        isPassed = true;
    }
}
