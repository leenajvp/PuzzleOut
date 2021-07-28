using UnityEngine;

public class NPCSounds : MonoBehaviour
{
    [Header("NPC Sounds")]
    [SerializeField] private AudioSource DraggingFeet = null;
    [SerializeField] private AudioSource Mumbles = null;
    [SerializeField] private AudioSource Attack = null;
    [SerializeField] private AudioSource Wonder = null;
    [SerializeField] private AudioSource GiveKey = null;
    [SerializeField] private AudioSource TongueIdle = null;
    [SerializeField] private AudioSource TongueIn = null;

    private Animator AnimState => GetComponent<Animator>();
    private int CurrentAnim => AnimState.GetInteger("AnimState");

    void Update()
    {
        // walk
        if (DraggingFeet != null)
        {
            if (CurrentAnim == 1 && DraggingFeet.isPlaying == false)
            {
                DraggingFeet.Play();
            }

            if (CurrentAnim != 1 && DraggingFeet.isPlaying == true)
            {
                DraggingFeet.Stop();
            }
        }

        if (Mumbles != null)
        {
            if (CurrentAnim == 1 && DraggingFeet.isPlaying == false)
            {
                Mumbles.Play();
            }

            if (CurrentAnim != 1 && DraggingFeet.isPlaying == true)
            {
                Mumbles.Stop();
            }

            //idle

            if (CurrentAnim == 0 && Mumbles.isPlaying == false)
            {
                Mumbles.Play();
            }

            if (CurrentAnim != 0 && Mumbles.isPlaying == true)
            {
                Mumbles.Stop();
            }
        }

        if (Wonder != null)
        {
            //receiving object

            if (CurrentAnim == 2 && Wonder.isPlaying == false)
            {
                Wonder.Play();
            }

            if (CurrentAnim != 2 && Wonder.isPlaying == true)
            {
                Wonder.Stop();
            }
        }

        if (GiveKey != null)
        {
            //give key

            if (CurrentAnim == 4 && GiveKey.isPlaying == false)
            {
                GiveKey.Play();
            }

            if (CurrentAnim != 4 && GiveKey.isPlaying == true)
            {
                GiveKey.Stop();
            }
        }

        if (TongueIdle != null)
        {
            // Tongue Idle

            if (CurrentAnim == 5 && TongueIdle.isPlaying == false)
            {
                TongueIdle.Play();
            }

            if (CurrentAnim != 5 && TongueIdle.isPlaying == true)
            {
                TongueIdle.Stop();
            }
        }

        if (TongueIn != null)
        {
            //tongue in

            if (CurrentAnim == 6 && TongueIn.isPlaying == false)
            {
                TongueIn.Play();
            }

            if (CurrentAnim != 6 && TongueIn.isPlaying == true)
            {
                TongueIn.Stop();
            }
        }

        if (Attack != null)
        {
            //attack

            if (CurrentAnim == 3 && Attack.isPlaying == false)
            {
                Attack.Play();
            }

            if (CurrentAnim != 3 && Attack.isPlaying == true)
            {
                Attack.Stop();
            }
        }
    }
}
