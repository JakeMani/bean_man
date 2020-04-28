using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity = 0.0f;
    private Vector3 horizontalDistanceToMove = Vector3.zero;

    private enum Lane
    {
        LeftLane,
        MiddleLane,
        RightLane
    }

    private Lane CurrentLane = Lane.MiddleLane;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpHeight = 10.0f;
    [SerializeField]
    private float gravity = 1.0f;
    [SerializeField]
    private float laneLength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Player always moves forward
        Vector3 direction = Vector3.forward;

        //  Calculate the velocity
        Vector3 velocity = direction * speed;

        //  Handle jump movement
        MoveJump();

        //  Update the y velocity with vertical velocity
        velocity.y = verticalVelocity;

        //  Calculate the distance to move
        Vector3 distanceToMove = velocity * Time.deltaTime;

        //  Handle the movement between lanes
        MoveLane();

        //  Update the x distance to move with horizontal distance to move
        distanceToMove.x = horizontalDistanceToMove.x;

        //  Move the character
        controller.Move(distanceToMove);
    }

    private void MoveJump()
    {
        //  Check if jump is allowed
        if (controller.isGrounded == true)
        {
            //  Check if space key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            //  Add gravity
            verticalVelocity -= gravity;
        }
    }

    private void MoveLane()
    {
        //  Reset the horizontal distance to move
        horizontalDistanceToMove = Vector3.zero;
        switch (CurrentLane)
        {
            case Lane.LeftLane:
                //  Move from left lane to middle lane
                if (Input.GetKeyDown(KeyCode.D))
                {
                    horizontalDistanceToMove = Vector3.right * laneLength;
                    CurrentLane = Lane.MiddleLane;
                }
                break;
            case Lane.MiddleLane:
                //  Move from middle lane to left lane
                if (Input.GetKeyDown(KeyCode.A))
                {
                    horizontalDistanceToMove = Vector3.left * laneLength;
                    CurrentLane = Lane.LeftLane;
                }
                //  Move from middle lane to right lane
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    horizontalDistanceToMove = Vector3.right * laneLength;
                    CurrentLane = Lane.RightLane;
                }
                break;
            case Lane.RightLane:
                //  Move from right lane to middle lane
                if (Input.GetKeyDown(KeyCode.A))
                {
                    horizontalDistanceToMove = Vector3.left * laneLength;
                    CurrentLane = Lane.MiddleLane;
                }
                break;
            default:
                //  No movement
                break;
        }
    }
}