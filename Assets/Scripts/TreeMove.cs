using UnityEngine;

public class TreeMove : MonoBehaviour
{
    public float interactDistance = 10f;
    public float lookTime = 5f;
    public float moveDistance = 3f;
    public float moveSpeed = 3f;

    private Transform player;
    private Vector3 targetPosition;
    private bool isLooking = false;
    private bool isMoving = false;
    private float lookTimer = 0f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
            return;
        }

        if (isLooking)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist <= interactDistance)
            {
                lookTimer += Time.deltaTime;
                if (lookTimer >= lookTime)
                {
                    lookTimer = 0f;
                    isLooking = false;
                    PickRandomDirection();
                }
            }
            else
            {
                lookTimer = 0f;
            }
        }
    }

    void PickRandomDirection()
    {
        int dirIndex = Random.Range(0, 4);
        Vector3 dir = Vector3.forward;

        if (dirIndex == 0) dir = Vector3.forward;
        else if (dirIndex == 1) dir = Vector3.back;
        else if (dirIndex == 2) dir = Vector3.left;
        else if (dirIndex == 3) dir = Vector3.right;

        targetPosition = transform.position + dir * moveDistance;
        targetPosition.y = Terrain.activeTerrain.SampleHeight(targetPosition) + Terrain.activeTerrain.transform.position.y;
        isMoving = true;
    }

    public void OnPointerEnter()
    {
        isLooking = true;
    }

    public void OnPointerExit()
    {
        isLooking = false;
        lookTimer = 0f;
    }
}
