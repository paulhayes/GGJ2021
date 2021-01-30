﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fling : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] float forceMultiplier;
    Transform arrowAsset;
    [SerializeField] float maxForce;

    [SerializeField] Transform ArrowPrefab;
    Vector3 startPos;
    Vector3 currentPos;
    bool down;
    Vector3 localSpaceStartPos;
    Rigidbody body;
    Camera cam;

    void Awake()
    {
        arrowAsset = Instantiate(ArrowPrefab);
        arrowAsset.gameObject.SetActive(false);
    }

    void Update()
    {
        if(down){
            Debug.DrawLine(startPos, currentPos,Color.green,0.2f );    

                    
    
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        //currentPos = eventData.pointerCurrentRaycast.worldPosition;
        //currentPos.z = startPos.z;
        Plane plane = new Plane(-Vector3.forward,startPos);
        float distance;
        //Debug.Log($"{eventData.position} {eventData.pointerCurrentRaycast.screenPosition}");
        var ray = cam.ScreenPointToRay(eventData.position);
        if( plane.Raycast(ray,out distance) ){
            currentPos = ray.origin + ray.direction * distance;
        }
        var direction = (currentPos-startPos).normalized;

        var midPoint = 0.5f * ( startPos + currentPos );
        arrowAsset.position = startPos + (cam.transform.position-startPos).normalized;
        arrowAsset.rotation = Quaternion.FromToRotation(Vector3.left,direction);
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //pull
        startPos = eventData.pointerCurrentRaycast.worldPosition;
        localSpaceStartPos = eventData.pointerCurrentRaycast.gameObject.transform.InverseTransformPoint( eventData.pointerCurrentRaycast.worldPosition );
        body = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Rigidbody>();
        Debug.Log("pointer down");
        arrowAsset.gameObject.SetActive(true);
        down = true;
        cam = eventData.pressEventCamera;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        //release
        down = false;
        arrowAsset.gameObject.SetActive(false);

        DoFling(eventData);
    }

    void DoFling(PointerEventData eventData)
    {
        var endPos = eventData.pointerCurrentRaycast.worldPosition;
        endPos.z = startPos.z;
        var force =  Mathf.Min( maxForce, Vector3.Distance(startPos,endPos)*forceMultiplier );
        var forceDir = -(endPos-startPos).normalized;
        
        if(body){
            var pos = body.transform.TransformPoint(localSpaceStartPos);
            Debug.DrawRay( pos, forceDir, Color.red, 1f );
            body.AddForceAtPosition(forceDir*force, pos, ForceMode.Impulse );
        }
        else {
            Debug.LogError("no rigidbody found on character?");
        }
    }
}
