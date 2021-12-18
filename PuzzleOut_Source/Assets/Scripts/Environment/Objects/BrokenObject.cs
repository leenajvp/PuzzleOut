using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class BrokenObject : MonoBehaviour
{
    [SerializeField]
    AudioSource breakSound;

    void Start()
    {
        breakSound = GetComponent<AudioSource>();
        breakSound.playOnAwake = false;
        breakSound.Play();
    }
}
