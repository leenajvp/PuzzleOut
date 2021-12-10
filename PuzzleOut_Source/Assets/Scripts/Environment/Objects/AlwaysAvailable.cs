using UnityEngine;

public class AlwaysAvailable : MonoBehaviour, IInteractive
{
    public bool isAvailable { get; set; }

    private void Start()
    {
        isAvailable = true;
    }
}
