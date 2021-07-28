using UnityEngine;

[RequireComponent(typeof(IActivator))]

public class ActivateNext : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private AudioSource soundToPlay;
    private IActivator Activator => GetComponent<IActivator>();

    private void Start()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Activator.isPassed == true)
        {
            ActivateNextRoom();
        }
    }

    private void ActivateNextRoom()
    {
        if (Activator.isPassed == true)
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
                Activator.isPassed = false;
            }

            if (soundToPlay != null && soundToPlay.isPlaying == false)
            {
                soundToPlay.Play();
            }

            else
            {
                return;
            }
        }
    }
}
