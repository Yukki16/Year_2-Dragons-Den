using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchToScene : MonoBehaviour
{
    [SerializeField] private string targetScene;
    public GameObject button;
    public UnityEvent unityEvent = new UnityEvent();

    void Start()
    {
        button = this.gameObject;
    }

    void Update()
    {
        
    }
}
