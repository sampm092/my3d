using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // From Unity
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        isGrounded = controller.isGrounded; // isGrounded = Was the CharacterController touching the ground during the last move?
    }

    // Menerima input dari InputManager.cs dan mengaplikasikannya pada Input Controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero; // Initialize moveDirection value (0f,0f,0f)
        moveDirection.x = input.x; // set moveDirection to (input.x,0,0)
        moveDirection.z = input.y; // set moveDirection to (input.x,0,input.y)

        //Move the character in the direction the player is facing [transform.TransformDirection(moveDirection)],
        // at a given speed [... * speed],
        // scaled by frame time [... * Time.deltaTime],
        // while respecting collisions [controller.Move(...)].
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime; // Apply gravity

        if (isGrounded && playerVelocity.y < 0) //If the player is on the ground and currently falling
            playerVelocity.y = -2f; // create vertical velocity to keep player standing on the ground

        controller.Move(playerVelocity * Time.deltaTime); //Moves the character vertically (gravity and jump)
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded) //if touching ground
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * gravity * -3.0f);
        }
    }
}
