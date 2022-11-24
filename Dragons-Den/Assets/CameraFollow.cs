using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followTarget;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 tp = followTarget.transform.position;
        transform.position = new Vector3(tp.x, tp.y, transform.position.z);
    }
}
