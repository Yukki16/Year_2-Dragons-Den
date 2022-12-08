using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followTarget;

    [SerializeField] private float boundRight;
    [SerializeField] private float boundLeft;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 tp = followTarget.transform.position;
        if (transform.position.x < boundRight && transform.position.x > boundLeft)     
            transform.position = new Vector3(tp.x, transform.position.y, transform.position.z);
           
        if (transform.position.x >= boundRight)
            transform.position = new Vector3(boundRight -0.01f, transform.position.y, transform.position.z);

        if (transform.position.x <= boundLeft)
            transform.position = new Vector3(boundLeft + 0.01f, transform.position.y, transform.position.z);
    }
}
