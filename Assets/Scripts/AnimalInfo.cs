using System.Collections.Generic;
using UnityEngine;

public class AnimalInfo : MonoBehaviour
{
    public static readonly List<AnimalInfo> AllAnimals = new List<AnimalInfo>();

    public string animalName;
    [TextArea(4, 10)]
    public string description;
    public Sprite image;
    public float interactDistance = 5f;

    void OnEnable()
    {
        if (!AllAnimals.Contains(this))
            AllAnimals.Add(this);
    }

    void OnDisable()
    {
        AllAnimals.Remove(this);
    }
}
