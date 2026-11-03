using ithappy.Animals_FREE;
using UnityEngine;

public class DeerRunWhenPlayerApprouch : MonoBehaviour
{
    public float runSpeed = 7f;
    public float runDuration = 3f;
    public float gravity = -9.8f;

    public AudioSource scaredSource;
    public AudioSource runAwaySource;

    private CharacterController characterController;
    private CreatureMover creatureMover;
    private AnimalWander animalWander;
    private Animator animator;
    private bool isRunning;
    private float runTimer;
    private float velocityY;
    private Vector3 runDirection;

    public bool IsRunning => isRunning;

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
        creatureMover = GetComponent<CreatureMover>();
        animalWander = GetComponent<AnimalWander>();
        runDirection = transform.forward;

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

            Vector3 away = transform.position - other.transform.position;
            away.y = 0f;
            runDirection = away.sqrMagnitude > 0.01f ? away.normalized : transform.forward;

            isRunning = true;
            runTimer = runDuration;
        }
    }

    void Update()
    {
        if (isRunning)
        {
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

        if (creatureMover != null && creatureMover.enabled)
        {
            if (isRunning)
            {
                Vector3 runTarget = transform.position + Vector3.up * 1f + runDirection * 10f;
                creatureMover.SetInput(new Vector2(0f, 1f), runTarget, true, false);
            }
            else if (animalWander == null || !animalWander.isActiveAndEnabled)
            {
                Vector3 lookAhead = transform.position + Vector3.up * 1f + transform.forward * 2f;
                creatureMover.SetInput(Vector2.zero, lookAhead, false, false);
            }

            return;
        }

        if (characterController.isGrounded && velocityY < 0f)
        {
            velocityY = -2f;
        }

        Vector3 move = isRunning ? runDirection * runSpeed : Vector3.zero;

        velocityY += gravity * Time.deltaTime;
        move.y = velocityY;

        characterController.Move(move * Time.deltaTime);
    }
}
