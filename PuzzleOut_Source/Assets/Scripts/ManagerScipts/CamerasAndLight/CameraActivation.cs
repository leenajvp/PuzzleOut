using UnityEngine;

public class CameraActivation : MonoBehaviour
{
    [SerializeField] private int cameraNum = 0;
    private ViewManager ViewManager => FindObjectOfType<ViewManager>();

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<IInteractive>().isAvailable == true)
        {
            ViewManager.SelectCamera(cameraNum);
        }

        else
        {
            return;
        }
    }
}
