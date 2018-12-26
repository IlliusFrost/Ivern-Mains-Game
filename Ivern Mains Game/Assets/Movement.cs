using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer mySpriteRenderer;
    public Transform myTransform;

    private KeyCode myLeftKey;
    private KeyCode myRightKey;
    private KeyCode myUpKey;

    private float myWalkSpeed;
    private float myJumpHeight;

    private Vector2 myScale = new Vector2(9.414985f, 8.173605f); // want this to be const... sigh#...

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

    DirectionState myDirectionState;
    PlayerState myPlayerState;

    void Start () {
        myRightKey = KeyCode.D;
        myLeftKey = KeyCode.A;
        myUpKey = KeyCode.W;
        myWalkSpeed = 30.0f;
        myJumpHeight = 1000.0f;

        myDirectionState = DirectionState.eRight;
        myPlayerState = PlayerState.eGrounded;

        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
    }
	
	void FixedUpdate () {
        UpdateMovement();
        UpdateFacedDirection();
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

        if (myRigidBody.velocity.y == 0.0f)
        {
            myPlayerState = PlayerState.eGrounded;
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

    private void UpdateFacedDirection()
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
