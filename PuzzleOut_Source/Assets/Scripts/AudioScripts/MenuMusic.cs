using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource menuMusic;

    public void FadeMusic()
    {
        menuMusic = GetComponent<AudioSource>();

        menuMusic.volume -= Time.deltaTime * 1;
    }
}
