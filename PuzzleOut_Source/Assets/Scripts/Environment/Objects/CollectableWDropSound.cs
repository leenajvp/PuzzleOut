using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CollectableWDropSound : Collectable
{
    private AudioSource dropSound; 

    private new void Start()
    {
        base.Start();
        dropSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Environment"))
        {
            dropSound.Play();
        }
    }
}
