using System.Collections;
using UnityEngine;

public class ColourPuzzle : MonoBehaviour
{
    [Header("Light Puzzle")]
    public bool lightsMathced;
    [SerializeField] private GameObject yellowRoom = null;
    [SerializeField] private GameObject redRoom = null;
    [SerializeField] private GameObject purpleRoom = null;

    [Header("Unlocking the door 2")]
    [SerializeField] private GameObject secondDoor = null;
    [SerializeField] private bool doorUnLocked = false;
    private NPCMovement NpcScript => FindObjectOfType<NPCMovement>();
    [SerializeField] private GameObject npCharacter;
    [SerializeField] private GameObject savingImage = null;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource door2Open = null;

    public bool redCompl;
    public bool yelCompl;
    public bool purpCompl;

    private ColourLights _Red;
    private ColourLights _Yellow;
    private ColourLights _Purple;

    private void Awake()
    {
        _Red = redRoom.GetComponent<ColourLights>();
        _Yellow = yellowRoom.GetComponent<ColourLights>();
        _Purple = purpleRoom.GetComponent<ColourLights>();
    }

    private void Start()
    {
        if (npCharacter == null)
        {
            npCharacter = NpcScript.gameObject;
        }

        npCharacter.SetActive(false);

        if (PlayerPrefs.GetInt("First") == 1)
        {
            lightsMathced = false;
            doorUnLocked = false;
        }

        if (PlayerPrefs.GetInt("Second") == 1)
        {
            lightsMathced = true;
            doorUnLocked = true;
            secondDoor.GetComponent<ElectricDoor>().isUnlocked = true;
        }

        if ((PlayerPrefs.GetInt("First") == 0) && (PlayerPrefs.GetInt("Second") == 0))
        {
            lightsMathced = false;
            doorUnLocked = false;
        }
    }

    private void Update()
    {
        FinalDoorOpen();
    }

    private void FinalDoorOpen()
    {
        if (_Yellow.currentColor == _Yellow.correctColour && _Red.currentColor == _Red.correctColour && _Purple.currentColor == _Purple.correctColour)
        {
            lightsMathced = true;
        }

        if (lightsMathced == true)
        {
            doorUnLocked = true;
            UnlockDoor();
            npCharacter.SetActive(true);
            _Red.isAvailable = false;
            _Yellow.isAvailable = false;
            _Purple.isAvailable = false;
        }
    }

    private void UnlockDoor()
    {
        if (doorUnLocked == true)
        {
            if (PlayerPrefs.GetInt("Second") == 0)
            {
                StartCoroutine(Saving());
                PlayerPrefs.SetInt("Second", 1);
            }

            secondDoor.GetComponent<ElectricDoor>().isUnlocked = true;
            doorUnLocked = false;
        }
    }

    private IEnumerator Saving()
    {
        savingImage.SetActive(true);
        door2Open.Play();

        yield return new WaitForSeconds(5);

        savingImage.SetActive(false);

    }
}
