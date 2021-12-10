using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCollectable : Collectable, IInteractive, ICollectable
{
    [SerializeField] 
    private GameObject singlereplacement = null;
    [SerializeField] 
    private AudioSource breakingSound = null;

    public void BreakObject()
    {
        Instantiate(singlereplacement, transform.position, transform.rotation);
        breakThis = false;
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (breakThis == true && collision.gameObject.tag == ("Environment"))
        {
            BreakObject();

            if (breakingSound != null)
            {
                breakingSound.Play();
            }
        }
    }
}
