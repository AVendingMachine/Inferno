using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 direction;
    public CharacterController controller;
    public float moveSpeed = 10f;
    private float currentSpeed;
    public float jumpHeight = 7;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public Vector3 velocity;
    public float gravity = -29.81f;
    private bool wallRunning = false;
    private bool jumpRun = false;
    public bool movingAllowed = true;


    private void Start()
    {
        currentSpeed = moveSpeed;
    }
    void Update()
    {
        
        //Walking Code
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        direction = transform.right * xAxis + transform.forward * zAxis;

        if (movingAllowed)
        {
            controller.Move(direction * Time.deltaTime * currentSpeed);
        }
   
        //Ground Check
        if (wallRunning == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            jumpRun = false;
        }
        //Jumping Code
        if (isGrounded && velocity.y < 0 )

        {
            velocity.y = -10;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Wall Run 
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, transform.right, out hit, 1,groundMask) || Physics.Raycast(groundCheck.position, -transform.right, out hit, 1,groundMask))
        {
            if (jumpRun== false)
            {
                wallRunning = true;
                isGrounded = true;
            }
            

        }

        else
        {
            jumpRun = false;
            wallRunning = false;
        }
        if (Physics.Raycast(groundCheck.position, transform.right, out hit, 1) || Physics.Raycast(groundCheck.position, -transform.right, out hit, 1))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpRun = true;
                isGrounded = false;
            }
        }
    }
}

