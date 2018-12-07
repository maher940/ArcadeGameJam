using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private GameObject blood;
    private GameObject target;
    private NavMeshAgent navAgent;
    private Animator animator;
    private enum State { Default, Walking, Attacking, Dead }
    private State currentState;

    private AI ai;
    private AIHand[] hands;
    private Rigidbody[] rigidbodies;
    

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health < 0)
            {
                health = 0;
            }
        }
    }

    private void Awake()
    {
        blood = transform.Find("FlexSourceActor").gameObject;
        blood.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        
        ai = GetComponent<AI>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentState = State.Default;
        hands = GetComponentsInChildren<AIHand>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Kill(rigidbodies[0].transform, -transform.forward, 100.0f);
        }

        if (!navAgent.enabled)
        {
            return;
        }

        navAgent.destination = target.transform.position;

        if (navAgent.pathPending)
        {
            return;
        }

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            currentState = State.Attacking;
            animator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);
        }
        else if (currentState == State.Default)
        {
            currentState = State.Walking;
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
        }

        Vector3 modifiedTargetPos = target.transform.position;
        modifiedTargetPos.y = transform.position.y;
        transform.LookAt(modifiedTargetPos);

        navAgent.speed = currentState == State.Walking ? movementSpeed : 0.0f;
    }

    private void LateUpdate()
    {
        if (currentState != State.Attacking)
        {
            return;
        }

        foreach (AIHand hand in hands)
        {
            if (hand.PlayerHit)
            {
                hand.PlayerHit = false;
                target.GetComponent<Player>().OnGetHit(5);
                break;
            }
        }
    }
    
    void DoneAttacking()
    {
        currentState = State.Default;
    }

    public void Kill(Transform bodyPart, Vector3 direction, float forceAmount)
    {
        if (currentState == State.Dead)
        {
            return;
        }

        currentState = State.Dead;

        navAgent.enabled = false;
        animator.enabled = false;

        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb.gameObject == hands[0].gameObject || rb.gameObject == hands[1].gameObject)
            {
                continue;
            }
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        if (bodyPart != null)
        {
            Rigidbody r = bodyPart.GetComponent<Rigidbody>();

            if (bodyPart == null)
            {
                rigidbodies[0].AddForce(direction * forceAmount, ForceMode.Impulse);
                blood.transform.parent = rigidbodies[0].transform;
                blood.SetActive(true);
                blood.transform.localPosition = Vector3.zero;
            }
            else if (r != null)
            {
                //Destroy(bodyPart.GetComponent<CharacterJoint>());
                //bodyPart.parent = null;
                r.AddForce(direction * forceAmount, ForceMode.Impulse);
                blood.transform.parent = r.transform;
                blood.SetActive(true);
                blood.transform.localPosition = Vector3.zero;
            }
        }

        ScoreKeeper.instance.Score += (int)ScoreKeeper.ScoreType.Kill;
        AIManager.instance.KilledCount++;

        StartCoroutine(WaitToDie());
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(gameObject);
    }
}
