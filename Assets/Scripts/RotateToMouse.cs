using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    
    [SerializeField] float xRange = 90;
    [SerializeField] float yRange = 30;


    Quaternion startRot;

    void Awake()
    {
        startRot = transform.rotation;
    }

    void Update(){

        Vector2 viewPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler( Mathf.Lerp(-yRange,yRange,viewPoint.y), Mathf.Lerp(xRange,-xRange,viewPoint.x), 0 ) * startRot; 

    }
    
}
