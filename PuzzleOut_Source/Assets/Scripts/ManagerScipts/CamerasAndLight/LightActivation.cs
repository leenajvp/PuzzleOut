using UnityEngine;

public class LightActivation : MonoBehaviour
{
    [SerializeField] private int lightNum = 0;
    private ViewManager viewManager;

    private void Start()
    {
        viewManager = FindObjectOfType<ViewManager>();
    }

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<IInteractive>().isAvailable == true)
        {
            viewManager.SelectLight(lightNum);
        }

        else
        {
            return;
        }
    }
}
