using ithappy.Animals_FREE;
using UnityEngine;

public class DeerRunWhenPlayerApprouch : MonoBehaviour
{
    public float runSpeed = 7f;
    public float runDuration = 3f;
    public float gravity = -9.8f;

    private CharacterController characterController;
    private Animator animator;
    private bool isRunning;
    private float runTimer;
    private float velocityY;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            }
        }

        velocityY += gravity * Time.deltaTime;
        move.y = velocityY;

        characterController.Move(move * Time.deltaTime);
    }
}
