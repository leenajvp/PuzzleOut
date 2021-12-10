using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class TextFadeOut : MonoBehaviour
{
    [SerializeField] 
    private float existingTime = 0f;
    [SerializeField] 
    private float removeAfterSeconds = 1f;
    private CanvasGroup Txt;

    void Start()
    {
        Txt = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        existingTime += Time.deltaTime * 1;

        if (existingTime >= removeAfterSeconds)
        {
            Txt.alpha -= Time.deltaTime * 4;
        }
    }
}
