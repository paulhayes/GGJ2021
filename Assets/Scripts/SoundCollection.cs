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
}
