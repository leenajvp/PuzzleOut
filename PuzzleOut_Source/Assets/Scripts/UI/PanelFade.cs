using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class PanelFade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float zeroAlpha = 0f;
    [SerializeField] 
    private float speed = 0.1f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = zeroAlpha;
    }

    void Update()
    {
        if (isActiveAndEnabled == true)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
        }
    }
}
