using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInt : MonoBehaviour
{
    [SerializeField] IntAsset value;
    [SerializeField] TextMeshProUGUI textField;

    // Update is called once per frame
    void OnEnable()
    {
        textField.text = value.Value.ToString();
    }
}
