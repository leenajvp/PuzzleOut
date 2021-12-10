using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class MoveOnStart : MonoBehaviour
{
    [SerializeField] private float moveStrenght = 1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveStrenght * 10);
    }
}
