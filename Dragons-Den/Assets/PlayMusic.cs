using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] AudioManager am;

    void Start()
    {
        am.LoopSound("RaceMusic");
        am.Play("RaceMusic");
    }

    void Update()
    {
        
    }
}
