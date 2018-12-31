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
    private KeyCode             myLeftKeyAZERTY;
    private KeyCode             myUpKeyAZERTY;

    public float                myWalkSpeed;
    private float               myJumpHeight;

    DirectionState              myDirectionState;
    PlayerState                 myPlayerState;

    public enum DirectionState
    {
        eLeft = -1,
        eRight = 1
    };
    public enum PlayerState
    {
        eGrounded,
        eAirborne
    }

    void Start ()
    {
        myRightKey              = KeyCode.D;
        myLeftKey               = KeyCode.A;
        myUpKey                 = KeyCode.W;
        myLeftKeyAZERTY         = KeyCode.Q;
        myUpKeyAZERTY           = KeyCode.Z;

        myWalkSpeed             = 30.0f;

        myJumpHeight            = 1500.0f; // why does this have to be so large?


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
            Walk(DirectionState.eRight);
        }
        else if (Input.GetKey(myLeftKey) || Input.GetKey(myLeftKeyAZERTY))
        {
            Walk(DirectionState.eLeft);
        }
        else
        {
            myRigidBody.velocity *= new Vector2(0.7f, 1.0f);
        }

        if (Input.GetKey(myUpKey) || Input.GetKey(myUpKeyAZERTY))
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

    public void Jump()
    {
        if (myPlayerState == PlayerState.eGrounded)
        {
            myRigidBody.AddForce(new Vector2(0.0f, myJumpHeight));
            myPlayerState = PlayerState.eAirborne;
        }
    }

    public void Walk(DirectionState aDirection)
    {
        Walk((int)aDirection);
    }

    public void Walk(int aDirection)
    {
        Vector2 temp = new Vector2(myWalkSpeed * aDirection, myRigidBody.velocity.y);
        myRigidBody.velocity = temp;
        myDirectionState = (DirectionState)aDirection;
    }

    public void Face(DirectionState aDirection)
    {
        myDirectionState = aDirection;
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
