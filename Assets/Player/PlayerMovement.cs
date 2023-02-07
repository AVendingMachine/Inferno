using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMoving = false;
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
    public float maxSlideSpeed = 10f;
    public float gravity = -29.81f;
    private bool wallRunning = false;
    private bool jumpRun = false;
    public bool movingAllowed = true;
    public bool aimingDown = false;
    private bool sliding = false;
    private bool falling = false;
    private bool isCheckingFall = false;
    private bool isCheckingMove = false;
    private bool slidingMode = true;
    private bool crouchMode = false;
    private bool moving = false;
    private float fallingAmount = 0f;
    public GameObject mainCamera;
    float xAxis = 0;
    float zAxis = 0;


    IEnumerator FallingCheck()
    {
        isCheckingFall = true;
        Vector3 checkPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        if (checkPos.y > transform.position.y)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
        isCheckingFall = false;
        fallingAmount = checkPos.y - transform.position.y;
    }
    IEnumerator MovementCheck()
    {
        isCheckingMove = true;
        Vector3 checkPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        if (checkPos != transform.position)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        isCheckingMove = false;
    }
    private void Start()
    {
        currentSpeed = moveSpeed;
    }
    void Update()
    {
        if (!isCheckingMove)
        {
            StartCoroutine(MovementCheck());
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slidingMode = true;
            mainCamera.transform.localPosition -= new Vector3(0, 0.5f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            mainCamera.transform.localPosition += new Vector3(0, 0.5f, 0);
            fallingAmount = 0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (crouchMode)
            {
                currentSpeed = 0.5f * moveSpeed;
            }
            if (slidingMode)
            {
                if (isCheckingFall == false)
                {
                    StartCoroutine(FallingCheck());
                }

                if (!falling)
                {
                    currentSpeed = Mathf.Clamp(currentSpeed - Time.deltaTime * 10, 0, moveSpeed);
                }
                if (falling)
                {
                    currentSpeed = Mathf.Clamp(currentSpeed + Time.deltaTime * (fallingAmount * 2 + 1), 0, moveSpeed * maxSlideSpeed);
                }
                sliding = true;
            }
            if (!moving)
            {
                sliding = false;
                slidingMode = false;
                crouchMode = true;
            }
            
        }
        else
        {
            crouchMode = false;
            sliding = false;
        }
        if (aimingDown && !sliding && !crouchMode)
        {
            currentSpeed = 0.5f * moveSpeed;
        }
        if (!aimingDown && !sliding && !crouchMode)
        {
            currentSpeed =
                moveSpeed;
        }
        //Walking Code

       
        if (!sliding)
        {
            xAxis = Input.GetAxis("Horizontal");
            zAxis = Input.GetAxis("Vertical");
        }
        if (direction != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
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

