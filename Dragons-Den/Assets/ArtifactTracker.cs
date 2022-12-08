using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactTracker : MonoBehaviour
{
    private static bool vase;
    private static bool tablet;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
      
    }

    public static bool HasVase()
    {
        return vase;
    }

    public static void HasVase(bool conditon)
    {
        vase = conditon;
    }

    public static bool HasTablet()
    {
        return tablet;
    }

    public static void HasTablet(bool conditon)
    {
        tablet = conditon;
    }
}
