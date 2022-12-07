using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactTracker : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
