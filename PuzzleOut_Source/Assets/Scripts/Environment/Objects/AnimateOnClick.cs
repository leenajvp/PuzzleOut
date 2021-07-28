using UnityEngine;

[RequireComponent(typeof(AudioSource), (typeof(IInteractive)), typeof(Animator))]

public class AnimateOnClick : MonoBehaviour
{
    public bool isAvailable { get; set; }
    private Animator Anim => GetComponent<Animator>();
    private AudioSource Sound => GetComponent<AudioSource>();

    private IInteractive Availability => GetComponent<IInteractive>();

    private void OnMouseDown()
    {
        if (Availability.isAvailable == true)
        {
            Sound.Play();
            Anim.SetTrigger("play");
        }
    }
}
