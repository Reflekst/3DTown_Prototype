using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;

    public static int jumps;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool duringJump;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;


    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Move();

    }
    private void Move()
    {
        
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        float moveZ = Input.GetAxis("Vertical");

        if (isGrounded &&  velocity.y < 0)
        {
            duringJump = false;
            anim.ResetTrigger("Jump");
            velocity.y = -2f;
            anim.ResetTrigger("InAir");
        }
        else if (!isGrounded)
        {
                //anim.SetTrigger("InAir"); // Dynamiczna animacja opadania
            
            if (MoveScan.isLanding && velocity.y < 0)
            {
                anim.ResetTrigger("InAir");
            }
        }

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection == Vector3.zero)
            Idle();
        if (MoveScan.isWay || duringJump || Input.GetKeyDown(KeyCode.Space))
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                Walk();
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                Run();
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            controller.Move(moveDirection * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0.3f, 0.1f, Time.deltaTime);

    }
    private void Walk()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetFloat("Speed", 0.65f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        }
        moveDirection *= walkSpeed;

    }
    private void Run()
    {
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        moveDirection *= runSpeed;
    }

    private void Jump()
    {
        duringJump = true;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("Jump");
        jumps++;
    }

}