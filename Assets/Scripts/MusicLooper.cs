using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] SoundCollection collection;
    [SerializeField] IntAsset level;

    public void Update(){

        if( !source.isPlaying )
        {
            source.clip = collection.GetClip(level.Value);
            Debug.Log($"{source.clip.name} {level.Value}");
            source.Play();
        }
    }
}
