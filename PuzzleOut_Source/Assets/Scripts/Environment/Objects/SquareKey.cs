using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareKey : Collectable
{
    [Tooltip("Object type that is changed after being dropped")]
    public CollectablesData updateCollectableType;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
