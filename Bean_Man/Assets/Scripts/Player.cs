using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float yVelocity = 0.0f;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpHeight = 10.0f;
    [SerializeField]
    private float gravity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 1);
        Vector3 velocity = direction * speed;

        //  Check if jump is allowed
        if (controller.isGrounded == true)
        {
            //  Check if space key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpHeight;
            }
        }
        else
        {
            //  Add gravity
            yVelocity -= gravity;
        }

        //  Update the y velocity
        velocity.y = yVelocity;

        //  Move the character
        controller.Move(velocity * Time.deltaTime);
    }
}