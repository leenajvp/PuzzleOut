using UnityEngine;

[RequireComponent(typeof(IActivator))]

public class ActivateNext : MonoBehaviour
{
    [Header("Elements to be activated for puzzle")]
    [SerializeField] 
    private GameObject[] elements; //Array can hold anything for next puzzle, as an example clues or objects

    [Header("Sound Feedback")]
    [Tooltip("If the activation will trigger any sound in the next room ")]
    [SerializeField]
    private AudioSource soundToPlay;
    
    private IActivator activator;

    private void Start()
    {
        activator = GetComponent<IActivator>();

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (activator.isPassed == true)
        {
            ActivateNextRoom();
        }
    }

    private void ActivateNextRoom()
    {
        if (activator.isPassed == true)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].SetActive(true);
                activator.isPassed = false;
            }

            //
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
