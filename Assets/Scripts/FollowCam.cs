using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float minY = 2.8f;
    [SerializeField] float maxY = 300f;

    [SerializeField] float acceleration = 0.2f;
    [SerializeField] float maxSpeed = 5f;

    [SerializeField] float drag = 0.5f;

    [SerializeField] float targetYOffset = 1;

    float speed;
    void Start()
    {
        if(!followTarget){
            followTarget = Object.FindObjectOfType<Fling>().transform;
        }
    }

    void Update()
    {
        var camPos = transform.position;
        var targetPos = followTarget.position + new Vector3(0,targetYOffset,0);
        camPos.y = Mathf.Clamp(camPos.y,minY,maxY);
        targetPos.y = Mathf.Clamp(targetPos.y,minY,maxY);
        float diff = targetPos.y - camPos.y;

        if(diff==0){
            return;
        }
        
        speed = Mathf.MoveTowards(speed,diff,Time.deltaTime*acceleration); //no overshoot
        //speed = Mathf.MoveTowards(drag*speed,Mathf.Sign(diff)*maxSpeed,Time.deltaTime*acceleration);
        //Debug.Log($"{diff},{speed}");

        
        camPos.y += speed * Time.deltaTime;
        camPos.y = Mathf.Clamp(camPos.y,minY,maxY);
        transform.position = camPos;
    }
}
