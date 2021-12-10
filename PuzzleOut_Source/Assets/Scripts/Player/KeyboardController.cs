using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private KeyboardController Inputs => FindObjectOfType<KeyboardController>();

    [Header("Default Keyboard Controls")]
    public KeyCode forward = KeyCode.UpArrow;
    public KeyCode backward = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode drop = KeyCode.LeftControl;

    [Header("Player")]
    [SerializeField] MonoBehaviour target = null;

    private IControls playerCharacter;

    void Start()
    {
        playerCharacter = target as IControls;

        if (PlayerPrefs.GetInt("PlayerControls") == 1)
        {
            Inputs.forward = KeyCode.UpArrow;
            Inputs.backward = KeyCode.DownArrow;
            Inputs.right = KeyCode.RightArrow;
            Inputs.left = KeyCode.LeftArrow;
        }

        else
        {

            Inputs.forward = KeyCode.W;
            Inputs.backward = KeyCode.S;
            Inputs.right = KeyCode.D;
            Inputs.left = KeyCode.A;
        }
    }

    void Update()
    {
        if (Input.GetKey(forward)) playerCharacter.Forward();
        if (Input.GetKey(backward)) playerCharacter.Backward();
        if (Input.GetKey(right)) playerCharacter.Right();
        if (Input.GetKey(left)) playerCharacter.Left();
        if (Input.GetKey(drop)) playerCharacter.Drop();
    }


}
