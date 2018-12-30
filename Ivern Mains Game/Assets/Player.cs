using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D          myRigidBody;
    public SpriteRenderer       mySpriteRenderer;
    public Transform            myTransform;
    public CircleCollider2D     myCircleCollider;

    private KeyCode             myLeftKey;
    private KeyCode             myRightKey;
    private KeyCode             myUpKey;

    private float               myWalkSpeed;
    private float               myJumpHeight;

    DirectionState              myDirectionState;
    PlayerState                 myPlayerState;

    private enum DirectionState
    {
        eLeft = -1,
        eRight = 1
    };
    private enum PlayerState
    {
        eGrounded,
        eAirborne
    }

    void Start ()
    {
        myRightKey              = KeyCode.D;
        myLeftKey               = KeyCode.A;
        myUpKey                 = KeyCode.W;

        myWalkSpeed             = 30.0f;
        myJumpHeight            = 3000.0f; // why does this have to be so large?

        myDirectionState        = DirectionState.eRight;
        myPlayerState           = PlayerState.eGrounded;

        myRigidBody             = GetComponent<Rigidbody2D>();
        mySpriteRenderer        = GetComponent<SpriteRenderer>();
        myTransform             = GetComponent<Transform>();
        myCircleCollider        = GetComponent<CircleCollider2D>();
    }
	
	void FixedUpdate ()
    {
        UpdateMovement();
        UpdateDirection();
    }

    private void UpdateMovement()
    {
        if (Input.GetKey(myRightKey))
        {
            Vector2 temp = new Vector2(myWalkSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = temp;
            myDirectionState = DirectionState.eRight;
        }
        else if (Input.GetKey(myLeftKey))
        {
            Vector2 temp = new Vector2(-myWalkSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = temp;
            myDirectionState = DirectionState.eLeft;
        }
        else
        {
            myRigidBody.velocity *= new Vector2(0.7f, 1.0f);
        }

        if (Input.GetKey(myUpKey))
        {
            Jump();
        }

        if (myRigidBody.velocity.y == 0.0f) // has edge cases :iverncringe:, figure out if theres a tile collision callback for potential fix
        {
            myPlayerState = PlayerState.eGrounded;
        }

        if (myPlayerState == PlayerState.eAirborne)
        {
            myRigidBody.velocity -= new Vector2(0.0f, 1.0f);
        }
    }

    private void Jump()
    {
        if (myPlayerState == PlayerState.eGrounded)
        {
            myRigidBody.AddForce(new Vector2(0.0f, myJumpHeight));
            myPlayerState = PlayerState.eAirborne;
        }
    }

    private void UpdateDirection()
    {
        if (myDirectionState == DirectionState.eLeft)
        {
            mySpriteRenderer.flipX = true;
        }
        else if (myDirectionState == DirectionState.eRight)
        {
            mySpriteRenderer.flipX = false;
        }
    }
}
