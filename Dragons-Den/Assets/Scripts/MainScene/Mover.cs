using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField, Tooltip("Character move speed.")]
    float speed = 5;

    [SerializeField, Tooltip("Character acceleration.")]
    float walkAcceleration = 10;

    [SerializeField, Tooltip("Character deceleration.")]
    float walkDeceleration = 10;

    private BoxCollider2D boxCollider;

    private bool canWalk = false;
    private bool lockMovement;

    private Vector2 velocity;

    private Vector2 mouseTargetPosition;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        float height = transform.GetComponent<BoxCollider2D>().size.y;

        if (moveInputX == 0 && moveInputY == 0)
        {
            anim.SetTrigger("StopWalk");

        }

        if(moveInputX > 0)
        {
            anim.SetTrigger("WalkRight");
        }

        if (moveInputX < 0)
        {
            anim.SetTrigger("WalkLeft");
        }

        RaycastHit2D bottomRayCast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height / 4), -Vector2.up);
        RaycastHit2D topRayCast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + height / 4), -Vector2.up);

        canWalk = false;

        if (bottomRayCast.collider != null)
        {
            if (bottomRayCast.collider.tag is "Walkable")
            {
                canWalk = true;
            }
        }

        if (lockMovement)
            return;


        if (moveInputX != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInputX, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, walkDeceleration * Time.deltaTime);
        }

        //if (moveInputY > 0 && topRayCast.collider.tag is "Walkable")
        //{
        //    velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputY, walkAcceleration * Time.deltaTime);
        //}

        //else if (moveInputY < 0 && bottomRayCast.collider.tag is "Walkable")
        //{
        //    velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputY, walkAcceleration * Time.deltaTime);
        //}
        //else
        //{
        //    velocity.y = Mathf.MoveTowards(velocity.y, 0, walkDeceleration * Time.deltaTime);
        //}

        transform.Translate(velocity * Time.deltaTime);
    }

    public void LockMovement(bool condition)
    {
        lockMovement = condition;
    }
}
