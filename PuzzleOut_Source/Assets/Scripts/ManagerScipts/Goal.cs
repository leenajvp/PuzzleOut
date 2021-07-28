using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Goal : MonoBehaviour
{
    [Header("SoundEffects")]
    [SerializeField] private AudioSource ambient = null;
    [SerializeField] private AudioSource breathing = null;

    [Header("SoundEffects")]
    [SerializeField] private GameObject blackScreen = null;
    [SerializeField] private float endTimer = 0f;

    private Volume Volume => GetComponent<Volume>();
    private ColorAdjustments color;
    private ChromaticAberration ca;
    private LensDistortion ld;

    private bool gameEnd;
    private bool reverse;

    private SceneMngr sceneManager => FindObjectOfType<SceneMngr>();

    void Start()
    {
        blackScreen.SetActive(false);

        ColorAdjustments c;

        if (Volume.profile.TryGet<ColorAdjustments>(out c))
        {
            color = c;
        }

        ChromaticAberration a;

        if (Volume.profile.TryGet<ChromaticAberration>(out a))
        {
            ca = a;
        }

        LensDistortion d;

        if (Volume.profile.TryGet<LensDistortion>(out d))
        {
            ld = d;
        }
    }

    private void Update()
    {
        if (gameEnd == true)
        {
            breathing.volume += 0.1f * Time.deltaTime * 2;
            ambient.volume += 0.1f * Time.deltaTime * 2;
            color.postExposure.value -= 0.1f * Time.deltaTime * 6;
            ca.intensity.value += 0.1f * Time.deltaTime * 1;
            ld.intensity.value -= 0.1f * Time.deltaTime * 2;
            StartCoroutine(BlackScreenTimer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Camera")
        {
            gameEnd = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Camera")
        {
            gameEnd = false;
            reverse = true;
        }
    }

    private IEnumerator BlackScreenTimer()
    {
        yield return new WaitForSeconds(endTimer);

        blackScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        sceneManager.GameWon();
    }
}