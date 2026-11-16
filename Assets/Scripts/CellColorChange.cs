using UnityEngine;

public class CellColorChange : MonoBehaviour
{
    public float changeInterval = 2f;

    private Renderer[] renderers;
    private bool isLooking = false;
    private float timer = 0f;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (isLooking)
        {
            timer += Time.deltaTime;
            if (timer >= changeInterval)
            {
                timer = 0f;
                Color randomColor = new Color(Random.value, Random.value, Random.value);

                foreach (Renderer r in renderers)
                {
                    r.material.color = randomColor;
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
        timer = 0f;
    }
}
