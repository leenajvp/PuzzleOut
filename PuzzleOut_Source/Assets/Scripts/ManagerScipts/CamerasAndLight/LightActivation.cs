using UnityEngine;

public class LightActivation : MonoBehaviour
{
    [SerializeField] private int lightNum = 0;
    private ViewManager ViewManager => FindObjectOfType<ViewManager>();

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<IInteractive>().isAvailable == true)
        {
            ViewManager.SelectLight(lightNum);
        }

        else
        {
            return;
        }
    }
}
