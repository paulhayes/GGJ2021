using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fling : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector3 startPos;
    [SerializeField] float forceMultiplier;
    bool down;
    Vector3 localSpaceStartPos;
    Rigidbody body;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Debug.DrawLine(startPos, eventData.pointerCurrentRaycast.worldPosition,Color.green,0.2f );
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //pull
        startPos = eventData.pointerCurrentRaycast.worldPosition;
        localSpaceStartPos = eventData.pointerCurrentRaycast.gameObject.transform.InverseTransformPoint( eventData.pointerCurrentRaycast.worldPosition );
        body = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Rigidbody>();
        Debug.Log("pointer down");
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        //release
        down = false;
        
        DoFling(eventData);
    }

    void DoFling(PointerEventData eventData)
    {
        var endPos = eventData.pointerCurrentRaycast.worldPosition;
        endPos.z = startPos.z;
        var force = Vector3.Distance(startPos,endPos)*forceMultiplier;
        var forceDir = -(endPos-startPos).normalized;
        
        if(body){
            var pos = body.transform.TransformPoint(localSpaceStartPos);
            Debug.DrawRay( pos, forceDir*force, Color.red, 1f );
            body.AddForceAtPosition(forceDir*force, pos, ForceMode.Impulse );
        }
        else {
            Debug.LogError("no rigidbody found on character?");
        }
    }
}
