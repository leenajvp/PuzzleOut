using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject flashLight = null;
    [SerializeField] private float mSensitivity = 2;
    [SerializeField] private float maxDown = -60F;
    [SerializeField] private float maxUp = 60F;

    private float rotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mSensitivity;

        rotationY += Input.GetAxis("Mouse Y") * mSensitivity;
        rotationY = Mathf.Clamp(rotationY, maxDown, maxUp);

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        flashLight.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}

