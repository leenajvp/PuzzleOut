using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerController1 : MonoBehaviour, IControls, IPlayer
{
    [Header("Interaction Settings")]
    [SerializeField] 
    private float rayDistance = 3;
    [SerializeField] 
    private Camera playerCamera = null;
    public GameObject holdPos;
    [SerializeField] 
    private GameObject MousePointer = null;
    [SerializeField] 
    private Light playerLight = null;
    [SerializeField] 
    private float minIntensity = 5f;
    [SerializeField] 
    private float defaultIntensity = 5f;
    [SerializeField] 
    private float currentIntensity = 0f;

    [Header("Mouse Pointer Size")]
    [SerializeField] 
    private Vector2 defaultSize = new Vector2(0.1f, 0.1f);
    [SerializeField] 
    private Vector2 hoverSize = new Vector2(0.2f, 0.2f);

    [Header("Camera Controls")]
    public float currentSpeed = 10.0f;
    public float defaultspeed = 10.0f;
    [SerializeField] 
    private GameObject collectedObject = null;

    [Header("Camera bounce")]
    [SerializeField] 
    private float walkingBobbingSpeed = 1f;
    [SerializeField] 
    private float idleBobSpeed = 5f;
    [SerializeField] 
    private float forwardBob = 0.03f;
    [SerializeField] 
    private float reverseBob = 0.04f;
    [SerializeField] 
    private float idleBob = 10f;
    [SerializeField] 
    private float defaultPosY = 0;
    [SerializeField] 
    private float active = 0;

    [Header("Dialogue Text")]
    [SerializeField] 
    private GameObject informationPanel = null;
    [SerializeField] 
    private Text textArea = null;

    [Header("Sound Effects")]
    [SerializeField] 
    private AudioSource walking = null;

    [Header("Completed Puzzles manager")]
    public int completed;

    private RaycastHit hit;

    private void Start()
    {
        informationPanel.SetActive(false);
    }

    private void Update()
    {
        Idle();
        Sounds();
        RayCasting();
    }

    private void Sounds()
    {
        if (playerCamera.enabled == true)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                walking.Play();
            }

            else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && walking.isPlaying)
            {
                walking.Stop();
            }
        }

        if (playerCamera.enabled == false)
        {
            walking.Stop();
        }
    }

    public void Forward()
    {
        if (playerCamera.enabled == true)
        {
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
            active += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(active) * forwardBob, transform.localPosition.z);
        }
    }

    public void Backward()
    {
        if (playerCamera.enabled == true)
        {
            transform.position -= transform.forward * currentSpeed * Time.deltaTime;
            active += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(active) * reverseBob, transform.localPosition.z);
        }
    }

    public void Left()
    {
        if (playerCamera.enabled == true)
        {
            transform.position -= transform.right * currentSpeed * Time.deltaTime;
        }

    }

    public void Right()
    {
        if (playerCamera.enabled == true)
        {
            transform.position += transform.right * currentSpeed * Time.deltaTime;
        }
    }

    public void Drop()
    {
        if (collectedObject != null)
        {
            var inventory = GetComponent<PlayerInventory>().CollectedObjects;

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].Drop();
            }
        }

        else
        {
            return;
        }
    }

    public void Idle()
    {
        active += Time.deltaTime * idleBobSpeed;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(active) * idleBob, transform.localPosition.z);
    }

    void SetTextArea(string text)
    {
        textArea.text = text;
    }


    private void RayCasting()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayDistance))
        {
            if (hit.collider == null)
            {
                return;
            }

            else
            {
                var hitObject = hit.collider.GetComponent<IInteractive>();

                if (hitObject != null && hitObject.isAvailable == true)
                {
                    MousePointer.transform.localScale = hoverSize;
                }

                if (hitObject == null)
                {
                    MousePointer.transform.localScale = defaultSize;
                }

                var hitCollectable = hit.collider.GetComponent<ICollectable>();

                if (hitCollectable != null && Input.GetMouseButtonDown(0))
                {
                    hitCollectable.Collect();
                    collectedObject = hit.collider.gameObject;
                }

                var hitAudio = hit.collider.GetComponent<IAudio>();

                if (hitAudio != null)
                {
                    hitAudio.PlayAudio();
                }

                IDialogue dialogue = hit.collider.GetComponent<IDialogue>();

                if (dialogue != null && dialogue.dialogueAvailable == true)
                {
                    SetTextArea(dialogue.GetDialogue());
                    informationPanel.SetActive(true);
                }

                if (hit.collider != null)
                {
                    playerLight.intensity -= minIntensity * Time.deltaTime * 3;

                    if (playerLight.intensity <= minIntensity)
                    {
                        playerLight.intensity = minIntensity;
                    }

                    currentIntensity = minIntensity;
                }
            }
        }

        else
        {
            MousePointer.transform.localScale = defaultSize;

            playerLight.intensity += defaultIntensity * Time.deltaTime * 1;

            if (playerLight.intensity >= defaultIntensity)
            {
                playerLight.intensity = defaultIntensity;
            }

            currentIntensity = defaultIntensity;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
