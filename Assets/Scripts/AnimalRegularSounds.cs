using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public float minDelay = 2f;  
    public float maxDelay = 8f; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundsRandomly());
    }

    IEnumerator PlaySoundsRandomly()
    {
        while (true)
        {
            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
