using UnityEngine;

public class PlayerDeactivator : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected PlayerController1 playerControl;
    [SerializeField]
    protected CameraController cameraControl;

    protected void Start()
    {
        if (player == null)
        {
            player= FindObjectOfType<PlayerController1>().gameObject;
        }

        playerControl = player.GetComponent<PlayerController1>();
        cameraControl = player.GetComponent<CameraController>();
    }

    protected void PlayerDisable()
    {
        cameraControl.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    protected void PlayerReturn()
    {
        cameraControl.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
