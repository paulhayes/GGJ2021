using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] SoundCollection soundCollection;
    [SerializeField] AudioSource audioSource;

    public void Play()
    {
        audioSource.PlayOneShot(soundCollection.NextClip());
    }

    public void PlayRandom()
    {
        audioSource.PlayOneShot(soundCollection.RandomClip());
    }
}
