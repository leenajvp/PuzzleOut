using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCollectableType", menuName = "CollebtableBase")]
public class CollectablesData : ScriptableObject
{
    public enum ObjectType
    {
        Random,
        Triangle,
        Square,
        Circle,
        Key,
        Mirror
    }

    public ObjectType objectType = ObjectType.Random;

    [Header("AddForce amount when dropped")]
    public float throwStrenght;

    [Header("SFX")]
    public AudioClip dropSound;
    public float volume;

    [Header("PhysicsMaterial")]
    public PhysicMaterial setMaterial;

    [Header("BreakableObejcts")]
    [Tooltip("Called by Script Breakable object, if true, object will break on when colliding with walls or floor")]
    public bool isBreakable;
    public GameObject brokenReplacement;
}
