using UnityEngine;

public class RaptorController : MonoBehaviour
{
    public float interactDistance = 5f;
    public float lookTime = 5f;
    public float spawnRadius = 30f;
    public int scoreValue = 1;

    private Transform player;
    private ScoreManager scoreManager;
    private Vector3 startPosition;
    private bool isLooking = false;
    private float lookTimer = 0f;

    void Start()
    {
        startPosition = transform.position;
        player = FindFirstObjectByType<PlayerMovement>().transform;
        scoreManager = FindFirstObjectByType<ScoreManager>();

        TeleportRandomly();
    }

    void Update()
    {
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
                    scoreManager.AddScore(scoreValue);
                    TeleportRandomly();
                }
            }
            else
            {
                lookTimer = 0f;
            }
        }
    }

    public void TeleportRandomly()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 newPos = startPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
        newPos.y = Terrain.activeTerrain.SampleHeight(newPos) + Terrain.activeTerrain.transform.position.y;
        transform.position = newPos;
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
