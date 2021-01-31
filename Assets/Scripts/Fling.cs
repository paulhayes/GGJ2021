using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Fling : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] float forceMultiplier;
    Transform arrowAsset;
    [SerializeField] float maxForce = 800f;
    [SerializeField] float maxArrowScale = 4f;
    [SerializeField] LayerMask layerMask;


    [SerializeField] float minForce = 10f;
    [SerializeField] Transform ArrowPrefab;
    
    [SerializeField] bool clickAnywhere;

    [SerializeField] UnityEvent OnFling;
    Vector3 startPos;
    Vector3 currentPos;
    bool down;
    Vector3 localSpaceStartPos;
    Rigidbody body;
    Camera cam;



    float flingForce;

    void Awake()
    {
        arrowAsset = Instantiate(ArrowPrefab);
        arrowAsset.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!down && clickAnywhere){
            if(Input.GetMouseButtonDown(0)){
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if( Physics.SphereCast(ray,0.3f,out hitInfo,100f,layerMask,QueryTriggerInteraction.Ignore) && hitInfo.collider.GetComponentInParent<Fling>() ){
                    StartFling(hitInfo.point,hitInfo.collider.gameObject);
                }

            }
            
        }
        else if(down){
            if(Input.GetMouseButtonUp(0)){
                StopFling();
            }
            else {
                UpdateArrow();
            }
            
        }
    }

    private void UpdateArrow()
    {
        //currentPos = eventData.pointerCurrentRaycast.worldPosition;
        //currentPos.z = startPos.z;
        Plane plane = new Plane(-Vector3.forward,startPos);
        float distance;
        //Debug.Log($"{eventData.position} {eventData.pointerCurrentRaycast.screenPosition}");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( plane.Raycast(ray,out distance) ){
            currentPos = ray.origin + ray.direction * distance;
        }
        var direction = (currentPos-startPos).normalized;

        var midPoint = 0.5f * ( startPos + currentPos );
        arrowAsset.position = startPos + (cam.transform.position-startPos).normalized;
        arrowAsset.rotation = Quaternion.FromToRotation(Vector3.down,direction);
        flingForce =  Mathf.Clamp( Vector3.Distance(startPos,currentPos)*forceMultiplier, minForce, maxForce );
        arrowAsset.localScale = Vector3.one * maxArrowScale * Mathf.InverseLerp(0,maxForce,flingForce);
        arrowAsset.gameObject.SetActive(true);
    }

    void StartFling(Vector3 startPos, GameObject targetObject)
    {
        if(down){
            return;
        }
        this.startPos = startPos;
        localSpaceStartPos = targetObject.transform.InverseTransformPoint( startPos );
        body = targetObject.GetComponentInParent<Rigidbody>();
        
        down = true;
        cam = Camera.main;
        flingForce = 0;
    }

    void StopFling()
    {
        //release
        down = false;
        arrowAsset.gameObject.SetActive(false);

        DoFling();
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //pull
        StartFling(eventData.pointerCurrentRaycast.worldPosition,eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(down){
            StopFling();
        }
    }

    void DoFling()
    {
        var endPos = currentPos;
        var force =  flingForce; //Mathf.Min( maxForce, Vector3.Distance(startPos,endPos)*forceMultiplier );
        var forceDir = -(endPos-startPos).normalized;
        
        if(body){
            var pos = body.transform.TransformPoint(localSpaceStartPos);
            Debug.DrawRay( pos, forceDir, Color.red, 1f );
            body.AddForceAtPosition(forceDir*force, pos, ForceMode.Impulse );
        }
        else {
            Debug.LogError("no rigidbody found on character?");
        }

        OnFling.Invoke();
    }
}
