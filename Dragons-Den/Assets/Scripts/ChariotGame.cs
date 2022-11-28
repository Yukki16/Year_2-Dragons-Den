using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotGame : MonoBehaviour
{

    [SerializeField] GameObject PlayerChariot;
    [SerializeField] ButtonManager bm;

    private SpriteRenderer sq;

    private Vector2 movementAddition;

    void Start()
    {
        sq = PlayerChariot.GetComponentInChildren<SpriteRenderer>();
        movementAddition = PlayerChariot.transform.position;
    }

    void Update()
    {
        sq.transform.position = movementAddition;
    }
}
