using ithappy.Animals_FREE;
using UnityEngine;

public class DeerRunWhenPlayerApprouch : MonoBehaviour
{
    public float runSpeed = 7f;
    public float runDuration = 3f;
    public float gravity = -9.8f;

    [Header("Звуки оленя")]
    public AudioSource scaredSource;
    public AudioSource runAwaySource;

    private CharacterController characterController;
    private Animator animator;
    private bool isRunning;
    private float runTimer;
    private float velocityY;

    void Awake()
    {
        if (scaredSource != null)
        {
            scaredSource.playOnAwake = false;
            scaredSource.Stop();
        }

        if (runAwaySource != null)
        {
            runAwaySource.playOnAwake = false;
            runAwaySource.Stop();
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (!isRunning)
        {
            if (scaredSource != null)
            {
                scaredSource.Stop();
            }

            if (runAwaySource != null)
            {
                runAwaySource.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isRunning)
            {
                if (scaredSource != null)
                {
                    scaredSource.Play();
                }

                if (runAwaySource != null && !runAwaySource.isPlaying)
                {
                    runAwaySource.Play();
                }
            }

            isRunning = true;
            runTimer = runDuration;
        }
    }

    void Update()
    {
        if (characterController.isGrounded && velocityY < 0f)
        {
            velocityY = -2f;
        }

        Vector3 move = Vector3.zero;

        if (isRunning)
        {
            move = transform.forward * runSpeed;

            runTimer -= Time.deltaTime;
            if (runTimer <= 0f)
            {
                isRunning = false;

                if (runAwaySource != null && runAwaySource.isPlaying)
                {
                    runAwaySource.Stop();
                }
            }
        }

        velocityY += gravity * Time.deltaTime;
        move.y = velocityY;

        characterController.Move(move * Time.deltaTime);
    }
}
