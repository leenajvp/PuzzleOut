using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CollectableWDropSound : Collectable
{
    private AudioSource DropSound => GetComponent<AudioSource>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Environment"))
        {
            DropSound.Play();
        }
    }
}
