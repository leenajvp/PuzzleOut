using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Collectable : InteractiveObjects, ICollectable, IInteractive
{
    [SerializeField]
    protected enum ObjectType
    {
        Random,
        Triangle,
        Square,
        Circle,
        Key,
        Mirror
    }

    [SerializeField] protected ObjectType objectType = ObjectType.Random;

    [SerializeField] protected GameObject itemHoldPos;
    [Tooltip("Called by Script Breakable object, if true, object will break on when colliding with walls or floor")]
    [SerializeField] protected bool breakThis = false;
    [SerializeField] protected bool isCollected = false;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource CollectSound = null;

    [Header("Object AddForce When Dropped")]
    [SerializeField] protected float throwStrenght = 30f;

    protected Rigidbody Rb => gameObject.GetComponent<Rigidbody>();

    protected new void Start()
    {
        base.Start();

        isCollected = false;
        isAvailable = true;
        gameObject.name = objectType.ToString();

        if (itemHoldPos == null)
        {
            itemHoldPos = GameObject.FindGameObjectWithTag("HoldPos");
        }
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
            Rb.isKinematic = true;
            isCollected = true;

            if (CollectSound != null)
            {
                CollectSound.Play();
            }
        }

        else
        {
            return;
        }
    }

    public void Drop()
    {
        isCollected = false;
        var objectList = player.GetComponent<PlayerInventory>().CollectedObjects;
        Collectable cObject = GetComponent<Collectable>();

        objectList.Remove(cObject);

        transform.parent = null;
        Rb.isKinematic = false;
        Rb.AddForce(transform.forward * throwStrenght);
        breakThis = true;
    }
}
