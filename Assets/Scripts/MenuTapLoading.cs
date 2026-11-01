using System.Collections;
using UnityEngine;
using TMPro; 

public class TapToContinueAnimation : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float updateInterval = 0.4f;

    private string baseText = "tap to continue";
    private int dotCount = 0;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(AnimateDots());
    }

    IEnumerator AnimateDots()
    {
        while (true)
        {
            string dots = new string('.', dotCount);
            textComponent.text = baseText + dots;

            dotCount = (dotCount + 1) % 4;

            yield return new WaitForSeconds(updateInterval);
        }
    }
}