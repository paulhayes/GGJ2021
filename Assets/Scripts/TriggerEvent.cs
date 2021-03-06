﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent TriggerDownEvent;
    [SerializeField] string targetTag;
    [SerializeField] bool once = false;
    void OnTriggerEnter(Collider other)
    {
        if(string.Empty!=targetTag && !other.gameObject.CompareTag(targetTag)){
            return;
        }
        Debug.Log($"{name}.OnTriggerEnter {other.name}");
        TriggerDownEvent.Invoke();
        if(once) {
            Destroy(this);
        }
    }
}
