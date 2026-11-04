using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalInfoUI : MonoBehaviour
{
    public GameObject infoPanel;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Image animalImage;

    private Transform player;
    private PlayerMovement playerMovement;
    private bool isOpen;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        if (playerMovement != null)
            player = playerMovement.transform;

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseInfo();
            }
            else
            {
                AnimalInfo animal = GetClosestAnimal();
                if (animal != null)
                    ShowInfo(animal);
            }
        }
    }

    AnimalInfo GetClosestAnimal()
    {
        if (player == null) return null;

        AnimalInfo closest = null;
        float minDist = float.MaxValue;

        foreach (AnimalInfo animal in AnimalInfo.AllAnimals)
        {
            float dist = Vector3.Distance(player.position, animal.transform.position);
            if (dist <= animal.interactDistance && dist < minDist)
            {
                minDist = dist;
                closest = animal;
            }
        }

        return closest;
    }

    void ShowInfo(AnimalInfo animal)
    {
        infoPanel.SetActive(true);
        isOpen = true;

        if (titleText != null)
            titleText.text = animal.animalName;

        if (descriptionText != null)
            descriptionText.text = animal.description;

        if (animalImage != null)
        {
            animalImage.gameObject.SetActive(true);
            animalImage.overrideSprite = null;
            animalImage.sprite = animal.image;
            animalImage.color = Color.white;
            animalImage.preserveAspect = true;
        }

        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    void CloseInfo()
    {
        infoPanel.SetActive(false);
        isOpen = false;

        if (playerMovement != null)
            playerMovement.enabled = true;
    }
}
