using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private bool repeat;
    private float length, startPos;
    [SerializeField] GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        if (repeat)
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (repeat)
        {
            if (temp > startPos + length - length) startPos += length;
            else if (temp < startPos - length) startPos -= length;
        }
    

    }
}
