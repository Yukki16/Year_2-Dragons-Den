using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPortal : MonoBehaviour
{
    [SerializeField] GameObject raceStadium;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        Debug.Log(hit.collider.name);

        if (hit.collider != null)
        {
            Debug.Log("Clicked on Object!");
        }
    }
}
