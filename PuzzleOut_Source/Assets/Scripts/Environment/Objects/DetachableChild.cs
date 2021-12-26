using UnityEngine;

public class DetachableChild : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
    }
    private void Update()
    {
        if (transform.parent == null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
