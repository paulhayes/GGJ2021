using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRespawn : MonoBehaviour
{
    //if below a y position, respawn
    [SerializeField] float bottom = -10f;
    [SerializeField] Transform targetTransform;
    Vector3 startPos;
    GameObject duplicate;

    void Awake()
    {
        gameObject.SetActive(false);
        duplicate=Instantiate(gameObject,transform.position,transform.rotation);
        gameObject.SetActive(true);
    }
    void Update()
    {
        if(targetTransform.position.y < bottom){
            duplicate.SetActive(true);
            Destroy(gameObject);
        }
    }
}
