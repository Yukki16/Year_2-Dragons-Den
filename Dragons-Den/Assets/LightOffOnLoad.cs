using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightOffOnLoad : MonoBehaviour
{
    private Light2D light;
    void Start()
    {
        light = GetComponent<Light2D>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
