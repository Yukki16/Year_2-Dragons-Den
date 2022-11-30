using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotation : MonoBehaviour
{
    [SerializeField] ScreenOrientation screenOrientation;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = screenOrientation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
