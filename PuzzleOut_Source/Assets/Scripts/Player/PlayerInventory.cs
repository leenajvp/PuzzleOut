using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Collectable> CollectedObjects = new List<Collectable>();
    [SerializeField] 
    private GameObject dropItem = null;

    private void Update()
    {
        if (CollectedObjects.Count == 1)
        {
            dropItem.SetActive(true);
        }

        else
        {
            dropItem.SetActive(false);
        }
    }
}
