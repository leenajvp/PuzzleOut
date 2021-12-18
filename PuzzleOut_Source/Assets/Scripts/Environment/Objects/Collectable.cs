using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]

public class Collectable : MonoBehaviour, ICollectable, IInteractive
{
    public CollectablesData collectableType;

    [SerializeField] 
    private GameObject itemHoldPos;
    [SerializeField]
    private GameObject player;

    [Header("Get Player Inventory")]
    private PlayerInventory inventory;
    private Collectable collected;

    private Rigidbody rb;
    private bool breakThis = false;
    private bool isReady = false;
    protected AudioSource hitSound;
    private Collider col;

    public bool isAvailable { get; set; }

    protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isAvailable = true;
        gameObject.name = collectableType.objectType.ToString();


            hitSound = GetComponent<AudioSource>();
            hitSound.playOnAwake = false;
            hitSound.spatialBlend = 1f;
            hitSound.clip = collectableType.dropSound;
            hitSound.volume = collectableType.volume;
        

        col = GetComponent<Collider>();
        col.material = collectableType.setMaterial;

        if (player == null)
        {
            player = FindObjectOfType<PlayerController1>().gameObject;
        }

        inventory = player.gameObject.GetComponent<PlayerInventory>();
        collected = gameObject.GetComponent<Collectable>();

        if (itemHoldPos == null)
        {
            itemHoldPos = GameObject.FindGameObjectWithTag("HoldPos");
        }

        StartCoroutine(StartTimer());
    }

    public void Collect()
    {
        var objectList = inventory.CollectedObjects;

        if (objectList.Count == 0)
        {
            objectList.Add(collected);
            transform.parent = itemHoldPos.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            rb.isKinematic = true;
//            isCollected = true;
        }

        else
        {
            return;
        }
    }

    public void Drop()
    {
        var objectList = player.GetComponent<PlayerInventory>().CollectedObjects;
        Collectable cObject = GetComponent<Collectable>();

        objectList.Remove(cObject);
        breakThis = true;
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * collectableType.throwStrenght);
    }

    public void BreakObject()
    {
        Instantiate(collectableType.brokenReplacement, transform.position, transform.rotation);
        breakThis = false;
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isReady)
        {
            if (collectableType.isBreakable == true && breakThis == true && collision.gameObject.tag == ("Environment"))
            {
                BreakObject();
            }

            if (hitSound != null)
            {
                hitSound.Play();
            }
        }
    }


    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(4f);
        
        isReady = true;
    }
}
