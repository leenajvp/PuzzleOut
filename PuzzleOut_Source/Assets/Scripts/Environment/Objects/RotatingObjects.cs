using UnityEngine;

public class RotatingObjects : MonoBehaviour
{
    [SerializeField] float xRot = 0f;
    [SerializeField] float yRot = 0f;
    [SerializeField] float zRot = 0f;

    void Update()
    {
        transform.Rotate(xRot, yRot, zRot * Time.deltaTime);
    }
}
