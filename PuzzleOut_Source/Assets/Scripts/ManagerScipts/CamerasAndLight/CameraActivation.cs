using UnityEngine;

public class CameraActivation : MonoBehaviour
{
    [SerializeField] 
    private int cameraNum = 0;
    private ViewManager viewManager;

    private void Start()
    {
        viewManager = FindObjectOfType<ViewManager>();
    }

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<IInteractive>().isAvailable == true)
        {
            viewManager.SelectCamera(cameraNum);
        }

        else
        {
            return;
        }
    }
}
