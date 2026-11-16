using ithappy.Animals_FREE;
using UnityEngine;

public class CatSpin : MonoBehaviour
{
    public float interactDistance = 10f;
    public float lookTime = 1f;
    public float afterSpinTime = 2f;
    public float spinSpeed = 360f;

    private Transform player;
    private AnimalWander animalWander;
    private CreatureMover creatureMover;

    private bool isLooking = false;
    private bool isSpinning = false;
    private float lookTimer = 0f;
    private float afterTimer = 0f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        animalWander = GetComponent<AnimalWander>();
        creatureMover = GetComponent<CreatureMover>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (isLooking && dist <= interactDistance)
        {
            if (!isSpinning)
            {
                lookTimer += Time.deltaTime;
                if (lookTimer >= lookTime)
                {
                    isSpinning = true;
                    afterTimer = afterSpinTime;
                    animalWander.enabled = false;
                    creatureMover.enabled = false;
                }
            }
            else
            {
                afterTimer = afterSpinTime;
            }
        }
        else
        {
            lookTimer = 0f;
        }

        if (isSpinning)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

            if (!isLooking || dist > interactDistance)
            {
                afterTimer -= Time.deltaTime;
                if (afterTimer <= 0f)
                {
                    isSpinning = false;
                    lookTimer = 0f;
                    creatureMover.enabled = true;
                    animalWander.enabled = true;
                }
            }
        }
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
