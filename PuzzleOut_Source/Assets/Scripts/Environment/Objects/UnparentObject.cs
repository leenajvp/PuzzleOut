using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentObject : Collectable
{
public new void Collect()
    {
        base.Collect();

        if (transform.parent != null)
        {
            transform.parent = null;
        }
    }
}
