using ithappy.Animals_FREE;
using UnityEngine;

[RequireComponent(typeof(CreatureMover))]
public class AnimalWander : MonoBehaviour
{
    public float wanderRadius = 8f;
    public float stopDistance = 1.2f;

    public float minIdleTime = 2f;
    public float maxIdleTime = 6f;

    private CreatureMover mover;
    private DeerRunWhenPlayerApprouch deerRun;

    private Vector3 startPosition;
    private Vector3 targetPoint;
    private float idleTimer;
    private bool isWalking;
    private bool wasRunningAway;

    void Awake()
    {
        mover = GetComponent<CreatureMover>();
        if (mover != null)
        {
            mover.enabled = true;
        }

        if (TryGetComponent<MovePlayerInput>(out var playerInput))
        {
            playerInput.enabled = false;
        }

        deerRun = GetComponent<DeerRunWhenPlayerApprouch>();
    }

    void Start()
    {
        startPosition = transform.position;
        StartIdle();
    }

    void Update()
    {
        if (deerRun != null && deerRun.IsRunning)
        {
            wasRunningAway = true;
            return;
        }

        if (wasRunningAway)
        {
            wasRunningAway = false;
            StartIdle();
        }

        if (isWalking)
        {
            Vector3 flatDiff = targetPoint - transform.position;
            flatDiff.y = 0f;

            if (flatDiff.magnitude <= stopDistance)
            {
                StartIdle();
            }
            else
            {
                Vector3 lookTarget = new Vector3(targetPoint.x, transform.position.y + 1f, targetPoint.z);
                mover.SetInput(new Vector2(0f, 1f), lookTarget, false, false);
            }
        }
        else
        {
            Vector3 lookAhead = transform.position + Vector3.up + transform.forward * 2f;
            mover.SetInput(Vector2.zero, lookAhead, false, false);

            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                PickNewTarget();
            }
        }
    }

    void StartIdle()
    {
        isWalking = false;
        idleTimer = Random.Range(minIdleTime, maxIdleTime);
    }

    void PickNewTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        targetPoint = startPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
        isWalking = true;
    }
}
