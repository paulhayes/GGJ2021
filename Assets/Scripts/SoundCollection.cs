using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundCollection : ScriptableObject
{
    [SerializeField] AudioClip[] sounds;

    int index = 0;
    public AudioClip NextClip(){
        if(index>=sounds.Length){
            index=0;
        }
        return sounds[index++];
    }

    public AudioClip RandomClip(){
        int randomIndex = Random.Range(0, sounds.Length-1);
        return sounds[randomIndex];
    }

    public AudioClip GetClip(int index)
    {
        return sounds[index % sounds.Length];
        
    }
}
