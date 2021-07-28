using UnityEngine;

public class AudioActivation : MonoBehaviour, IAudio
{
    [SerializeField] protected bool isActivated;
    [SerializeField] private AudioSource AudioToPlay => GetComponent<AudioSource>();
    public bool isAvailable { get; set; }

    private void Start()
    {
        isAvailable = true;
        gameObject.layer = 13;
    }

    public void PlayAudio()
    {
        if (isAvailable == true)
        {
            AudioToPlay.Play();
            isActivated = true;
            isAvailable = false;
        }

        else
        {
            return;
        }
    }
}
