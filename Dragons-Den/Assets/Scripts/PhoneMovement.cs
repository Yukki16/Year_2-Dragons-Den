using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] Camera _camera;

    [SerializeField, Tooltip("Character move speed.")] float speed = 1;
    [SerializeField, Tooltip("Character acceleration.")] float walkAcceleration = 10;
    [SerializeField, Tooltip("Character deceleration.")] float walkDeceleration = 10;
    Vector2 velocity = new Vector2();

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
     
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > _camera.transform.position.x + 5 * _camera.scaledPixelWidth / 6 && Input.mousePosition.x < _camera.transform.position.x + _camera.scaledPixelWidth)
            {
                anim.SetTrigger("WalkRight");
                velocity.x = Mathf.MoveTowards(velocity.x, speed, walkAcceleration * Time.deltaTime);   
            }
            else if(Input.mousePosition.x < _camera.transform.position.x + _camera.scaledPixelWidth / 6 && Input.mousePosition.x > _camera.transform.position.x)
            {
                anim.SetTrigger("WalkLeft");
                velocity.x = Mathf.MoveTowards(velocity.x, -speed, walkAcceleration * Time.deltaTime);
            } 
        }
        else
        {
            anim.SetTrigger("StopWalk");
            velocity.x = Mathf.MoveTowards(velocity.x, 0, walkDeceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}
