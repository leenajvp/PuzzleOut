using UnityEngine;
public class AudioActivation : MonoBehaviour, IAudio
{
    [SerializeField] 
    protected bool isActivated;
    [SerializeField]
    private AudioSource audioToPlay; 
    public bool isAvailable { get; set; }

    private void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
        isAvailable = true;
        gameObject.layer = 13;
    }

    public void PlayAudio()
    {
        if (isAvailable == true)
        {
            audioToPlay.Play();
            isActivated = true;
            isAvailable = false;
        }

        else
        {
            return;
        }
    }
}
