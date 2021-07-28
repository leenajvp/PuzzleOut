using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class MoveOnStart : MonoBehaviour
{
    [SerializeField] private float moveStrenght = 1f;
    private Rigidbody Rb => GetComponent<Rigidbody>();

    private void Start()
    {
        Rb.AddForce(transform.forward * moveStrenght * 10);
    }
}
