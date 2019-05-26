using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource my_source;
    float my_lenght;
    void Start()
    {
        my_source = GetComponent<AudioSource>();
        my_lenght = my_source.clip.length;
        StartCoroutine(Play(my_lenght));
    }

    IEnumerator Play(float t)
    {
        my_source.Play();
        yield return new WaitForSeconds(t);
        int i = Random.Range(0, audioClips.Length);
        my_source.clip = audioClips[i];
        my_lenght = audioClips[i].length;
        StartCoroutine(Play(my_lenght));
    }
}
