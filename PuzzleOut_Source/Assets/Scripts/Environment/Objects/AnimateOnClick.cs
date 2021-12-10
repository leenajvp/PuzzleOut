using UnityEngine;

[RequireComponent(typeof(AudioSource), (typeof(IInteractive)), typeof(Animator))]

public class AnimateOnClick : MonoBehaviour
{
    public bool isAvailable { get; set; }
    private Animator anim;
    private AudioSource sound;

    private IInteractive availability;

    private void Start()
    {
        anim= GetComponent<Animator>();
        sound= GetComponent<AudioSource>();
        availability= GetComponent<IInteractive>();
    }

    private void OnMouseDown()
    {
        if (availability.isAvailable == true)
        {
            sound.Play();
            anim.SetTrigger("play");
        }
    }
}
