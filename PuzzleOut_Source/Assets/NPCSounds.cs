using UnityEngine;

public class NPCSounds : MonoBehaviour
{
    [Header("NPC Sounds")]
    [SerializeField]
    AudioSource draggingFeet = null;
    [SerializeField]
    AudioSource mumbles = null;
    [SerializeField]
    AudioSource attack = null;
    [SerializeField]
    AudioSource wonder = null;
    [SerializeField]
    AudioSource giveKey = null;
    [SerializeField]
    AudioSource tongueIdle = null;
    [SerializeField]
    AudioSource tongueIn = null;
    [SerializeField]
    AudioSource playerDead = null;

    private Animator animator;
    private int currentAnim;
    private static readonly int animState = Animator.StringToHash("AnimState");

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        currentAnim = animator.GetInteger(animState);

        // walk
        if (draggingFeet != null)
        {
            if (currentAnim == 1 && draggingFeet.isPlaying == false)
            {
                draggingFeet.Play();
            }

            if (currentAnim != 1 && draggingFeet.isPlaying == true)
            {
                draggingFeet.Stop();
            }
        }

        if (mumbles != null)
        {
            if (currentAnim == 1 && draggingFeet.isPlaying == false)
            {
                mumbles.Play();
            }

            if (currentAnim != 1 && draggingFeet.isPlaying == true)
            {
                mumbles.Stop();
            }

            //idle

            if (currentAnim == 0 && mumbles.isPlaying == false)
            {
                mumbles.Play();
            }

            if (currentAnim != 0 && mumbles.isPlaying == true)
            {
                mumbles.Stop();
            }
        }

        if (wonder != null)
        {
            //receiving object

            if (currentAnim == 2 && wonder.isPlaying == false)
            {
                wonder.Play();
            }

            if (currentAnim != 2 && wonder.isPlaying == true)
            {
                wonder.Stop();
            }
        }

        if (giveKey != null)
        {
            //give key

            if (currentAnim == 4 && giveKey.isPlaying == false)
            {
                giveKey.Play();
            }

            if (currentAnim != 4 && giveKey.isPlaying == true)
            {
                giveKey.Stop();
            }
        }

        if (tongueIdle != null)
        {
            // Tongue Idle

            if (currentAnim == 5 && tongueIdle.isPlaying == false)
            {
                tongueIdle.Play();
            }

            if (currentAnim != 5 && tongueIdle.isPlaying == true)
            {
                tongueIdle.Stop();
            }
        }

        if (tongueIn != null)
        {
            //tongue in

            if (currentAnim == 6 && tongueIn.isPlaying == false)
            {
                tongueIn.Play();
            }

            if (currentAnim != 6 && tongueIn.isPlaying == true)
            {
                tongueIn.Stop();
            }
        }

        if (attack != null)
        {
            //attack

            if (currentAnim == 3 && attack.isPlaying == false)
            {
                attack.Play();
            }

            if (currentAnim != 3 && attack.isPlaying == true)
            {
                attack.Stop();
            }
        }
    }
}
