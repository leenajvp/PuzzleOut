using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource menuMusic;

    //Fade out music when called
    public void FadeMusic()
    {
        menuMusic = GetComponent<AudioSource>();
        menuMusic.volume -= Time.deltaTime * 1;
    }
}
