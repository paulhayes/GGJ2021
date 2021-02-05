using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve curve;

    void OnEnable()
    {
        StartCoroutine(StartFadeCoroutine());
    }

    IEnumerator StartFadeCoroutine()
    {
        float duration = curve.keys[curve.keys.Length-1].time;
        float elapsed = 0;
        while(elapsed<duration){
            canvasGroup.alpha = curve.Evaluate(elapsed);
            yield return null;
            elapsed += Time.deltaTime;
        }
        canvasGroup.alpha = curve.Evaluate(duration);
        
    }
}
