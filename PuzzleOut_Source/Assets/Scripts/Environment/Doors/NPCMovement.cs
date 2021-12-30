using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCMovement : InteractiveObjects
{
    [SerializeField]
    private enum NpcState
    {
        Idle,
        MaintainDistance,
        CheckItem,
        Accept,
        WrongAnswer,
        GiveKey,
        Kill,
        MoveAway,
        Unavailable
    }

    [SerializeField] 
    private NpcState CurrentState;

    [Header("Maintain Distance from player")]
    [SerializeField] 
    private float minDistance = 3;
    [SerializeField] 
    private float interactionDistance = 1;
    [SerializeField] 
    private float distanceToPlayer;

    [Header("Set Targets")]
    [SerializeField] 
    private Transform[] targets = null;
    private int setTargets = 0;

    [Header("Final Question")]
    [SerializeField] 
    private GameObject finalQuestion = null;
    [SerializeField] 
    private GameObject key = null;
    [SerializeField] 
    private GameObject dropPos = null;
    [SerializeField] 
    private KeyCode yes = KeyCode.Alpha1;
    [SerializeField] 
    private KeyCode no = KeyCode.Alpha2;
    public bool giveKey;

    [Header("SoundEffects")]
    [Header("Player Reactions")]
    [SerializeField] 
    private AudioSource KeyReaction = null;
    [SerializeField] 
    private AudioSource DeadReaction = null;
    private int currentAnim;

    [Header("EndGame")]
    [SerializeField] 
    private GameObject blackPanel = null;
    private SceneMngr SceneManager;
    private GameMngr GameManager;

    private Animator animator;
    private static readonly int animState = Animator.StringToHash("AnimState");

    private NavMeshAgent Agent;

    private new void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        
        SceneManager = FindObjectOfType<SceneMngr>();
        GameManager = FindObjectOfType<GameMngr>();
        Agent = GetComponent<NavMeshAgent>();

        
        finalQuestion.SetActive(false);
        Physics.GetIgnoreLayerCollision(9, 8);
        blackPanel.SetActive(false);
        giveKey = true;
    }

    private void Update()
    {
        currentAnim = animator.GetInteger(animState);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (CurrentState)
        {
            case NpcState.Idle:

                StayStill();
                CheckForPlayer();

                break;

            case NpcState.MaintainDistance:

                Move();

                break;

            case NpcState.CheckItem:

                CheckInventory();

                break;

            case NpcState.Accept:

                ReceiveCorrect();

                break;

            case NpcState.WrongAnswer:

                ReceiveWrong();
                break;

            case NpcState.GiveKey:

                StartCoroutine(RevealKey());

                break;

            case NpcState.Kill:

                StartCoroutine(AttackPlayer());

                break;

            case NpcState.MoveAway:

                NegativeAnswerReceived();

                break;

            case NpcState.Unavailable:

                IgnorePlayer();

                break;

            default:

                CurrentState = NpcState.Idle;

                break;
        }
    }

    private void StayStill()
    {
        Agent.isStopped = true;
        animator.SetInteger(animState, 0);
    }

    private void CheckForPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.GetComponent<IPlayer>().GetPosition());

        if (minDistance >= distanceToPlayer)
        {
            CurrentState = NpcState.CheckItem;
        }

        if (minDistance <= distanceToPlayer)
        {
            CurrentState = NpcState.Idle;
        }
    }

    private void Move()
    {
        animator.SetInteger(animState, 1);
        finalQuestion.SetActive(false);
        CheckInventory();
        Agent.isStopped = false;

        while (distanceToPlayer <= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                Agent.destination = targets[setTargets].position;
                setTargets = Random.Range(0, targets.Length);
            }
            break;
        }

        if (distanceToPlayer >= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                CurrentState = NpcState.Idle;

            }
        }
    }

    private void CheckInventory()
    {

        if (inventory.CollectedObjects.Count != 0)
        {
            for (int i = 0; i < inventory.CollectedObjects.Count; i++)
            {
                if (inventory.CollectedObjects[i].name == _requiredObject)
                {
                    CurrentState = NpcState.Accept;
                }

                else if (inventory.CollectedObjects[i].name != _requiredObject)
                {
                    CurrentState = NpcState.WrongAnswer;
                }
            }
        }

        else
        {
            CurrentState = NpcState.MaintainDistance;
        }
    }

    private void LookAtTarget()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        this.transform.LookAt(playerPos);
    }

    private void ReceiveCorrect()
    {
        animator.SetInteger(animState, 2);

        if (inventory.CollectedObjects.Count == 0)
        {
            CurrentState = NpcState.MaintainDistance;
            finalQuestion.SetActive(false);
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Agent.isStopped = true;

        animator.SetInteger(animState, 2);
        Agent.isStopped = true;

        LookAtTarget();

        if (distanceToPlayer <= interactionDistance)
        {
            finalQuestion.SetActive(true);

            if (Input.GetKey(yes))
            {
                KeyReaction.Play();
                SetTextArea(DialogueTxt.GetDialogue());
                finalQuestion.SetActive(false);

                for (int i = 0; i < inventory.CollectedObjects.Count; i++)
                {
                    var correctObject = inventory.CollectedObjects[i].gameObject;
                    correctObject.SetActive(false);
                    inventory.CollectedObjects.Clear();
                }

                CurrentState = NpcState.GiveKey;
            }

            if (Input.GetKey(no))
            {
                CurrentState = NpcState.MaintainDistance;
                CurrentState = NpcState.MoveAway;
            }
        }

        else
        {
            finalQuestion.SetActive(false);
        }
    }

    private void ReceiveWrong()
    {
        animator.SetInteger(animState, 2);

        if (inventory.CollectedObjects.Count == 0)
        {
            CurrentState = NpcState.MaintainDistance;
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        animator.SetInteger(animState, 2);
        Agent.isStopped = true;

        LookAtTarget();

        if (distanceToPlayer <= interactionDistance)
        {
            finalQuestion.SetActive(true);

            if (Input.GetKey(yes))
            {
                finalQuestion.SetActive(false);
                CurrentState = NpcState.Kill;
            }

            if (Input.GetKey(no))
            {
                finalQuestion.SetActive(false);
                CurrentState = NpcState.MoveAway;
            }
        }

        else
        {
            finalQuestion.SetActive(false);
        }
    }

    private IEnumerator RevealKey()
    {
        animator.SetInteger(animState, 4);

        yield return new WaitForSeconds(2f);

        InstantiateKey();

        animator.SetInteger(animState, 5);

        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        for (int i = 0; i < inventory.CollectedObjects.Count; i++)
        {
            if (inventory.CollectedObjects[i].name == "Key")
            {
                animator.SetInteger(animState, 1);
                CurrentState = NpcState.Unavailable;
            }
        }
    }

    private void InstantiateKey()
    {
        if (giveKey == true)
        {
            Instantiate(key.gameObject, dropPos.transform.position, transform.rotation);
            giveKey = false;
        }

        else
        {
            return;
        }
    }

    private IEnumerator AttackPlayer()
    {
        GameManager.canPause = false;

        animator.SetInteger(animState, 3);

        if (currentAnim == 3 && DeadReaction.isPlaying == false)
        {
            DeadReaction.Play();
        }

        if (currentAnim != 3 && DeadReaction.isPlaying == true)
        {
            DeadReaction.Stop();
        }

        player.GetComponent<KeyboardController>().enabled = false;
        player.GetComponent<CameraController>().mSensitivity = 0;

        yield return new WaitForSeconds(1);

        GetComponent<Collider>().isTrigger = true;
        Agent.isStopped = false;
        Agent.destination = player.transform.position;

        yield return new WaitForSeconds(4);

        blackPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        SceneManager.GameLost();
    }

    private void IgnorePlayer()
    {
        Agent.isStopped = false;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        while (distanceToPlayer <= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                animator.SetInteger(animState, 1);
                Agent.destination = targets[setTargets].position;
                setTargets = Random.Range(0, targets.Length);
            }

            break;
        }

        if (distanceToPlayer >= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                animator.SetInteger(animState, 0);
            }
        }
    }

    private void NegativeAnswerReceived()
    {
        Agent.isStopped = false;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        finalQuestion.SetActive(false);
        animator.SetInteger(animState, 1);


        if (distanceToPlayer <= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                Agent.destination = targets[setTargets].position;
                setTargets = Random.Range(0, targets.Length);
            }
        }

        if (distanceToPlayer >= minDistance)
        {
            if (!Agent.pathPending && Agent.remainingDistance < 0.2f)
            {
                CurrentState = NpcState.Idle;
            }
        }
    }
}
