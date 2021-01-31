using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntAsset : ScriptableObject
{
    [SerializeField] int initialValue;
    [SerializeField] int value;

    public int Value {
        get { return value; }
        set { this.value = value; }
    }

    void OnEnable()
    {
        value = initialValue;
    }
}
