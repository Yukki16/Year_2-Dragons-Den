using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactReciever : MonoBehaviour
{

    [SerializeField] private GameObject romeVase;
    [SerializeField] private GameObject romeTablet;

    void Start()
    {

    }

    void Update()
    {
        if (ArtifactTracker.HasVase())
        {
            romeVase.transform.GetChild(0).gameObject.SetActive(false);
            romeVase.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (ArtifactTracker.HasTablet())
        {
            romeTablet.transform.GetChild(0).gameObject.SetActive(false);
            romeTablet.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
