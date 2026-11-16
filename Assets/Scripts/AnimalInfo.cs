using System.Collections.Generic;
using UnityEngine;

public class AnimalInfo : MonoBehaviour
{
    public static readonly List<AnimalInfo> AllAnimals = new List<AnimalInfo>();

    public string animalName;
    [TextArea(4, 10)]
    public string description;
    public Sprite image;
    public float interactDistance = 8f;
    public float lookTime = 2.5f;
    public bool isRead = false;

    private float lookTimer = 0f;
    private bool isLooking = false;
    private Transform player;
    private AnimalInfoUI infoUI;

    void OnEnable()
    {
        if (!AllAnimals.Contains(this))
            AllAnimals.Add(this);
    }

    void OnDisable()
    {
        AllAnimals.Remove(this);
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        infoUI = FindFirstObjectByType<AnimalInfoUI>();
    }

    void Update()
    {
        if (isLooking && !infoUI.isOpen)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist <= interactDistance)
            {
                lookTimer += Time.deltaTime;
                if (lookTimer >= lookTime)
                {
                    lookTimer = 0f;
                    isLooking = false;
                    infoUI.ShowInfo(this);
                }
            }
            else
            {
                lookTimer = 0f;
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
