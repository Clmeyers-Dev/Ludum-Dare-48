using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public PlayerController controller;
    // Use this for initialization
    public float moveSpeed = 10f;
    private float attackTime = .25f;
    private float attackCounter = .25f;
    public float horiztonalMove = 0f;
    public float vertMove = 0f;
    //private float rollCounter = .45f;dd
    private bool isAttacking;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isRolling;
    public float speed = 0;
    public Vector3 lastPosition;
    //private BarManager BarMan;
    bool jump = false;
    private bool isGrounded;
    public float lastMove = 0;
    [SerializeField]
    private Transform groundPoint;
    [SerializeField]
    private float groundRadius = 0.1f;
    [SerializeField]
    private LayerMask whatIsGround;
    public bool crouch;
    public void Start()
    {
       
    }
    
    void Update()
    {
        setLastMove(Input.GetAxisRaw("Horizontal"));
        vertMove = Input.GetAxisRaw("Vertical") * moveSpeed;
        horiztonalMove = Input.GetAxisRaw("Horizontal")*moveSpeed;
        HandleAnimations();
        if(Input.GetKey(KeyCode.LeftShift)){
            crouch = true;
        }else{
            crouch = false;
        }
       
    }
    private void HandleAnimations()
    {
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);

            //Set the animator velocity equal to 1 * the vertical direction in which the player is moving 
            animator.SetFloat("Vertical", 1 * Mathf.Sign(rb.velocity.y));
        }

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetFloat("Vertical", 0);
        }

        animator.SetFloat("Horizontal", horiztonalMove);
        animator.SetFloat("Speed", Mathf.Abs(horiztonalMove));
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);

            //Set the animator velocity equal to 1 * the vertical direction in which the player is moving 
            animator.SetFloat("Vertical", 1 * Mathf.Sign(rb.velocity.y));
        }

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetFloat("Vertical", 0);
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isJumping", true);
        }
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            setLastMove(Input.GetAxisRaw("Horizontal"));
        }

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }
    private void HandleMovement()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        controller.Move(horiztonalMove * Time.deltaTime, crouch, jump);
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
      
    }
    public float getSpeed()
    {
        return speed;
    }
    public void setMoveSpeed( float movespeed)
    {
        if (movespeed > 0)
        {
            moveSpeed = movespeed;
        }
    }
    public void setLastMove(float move)
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            lastMove = move;
        }
            
    }
    public float getLastMove()
    {
        return lastMove;
    }
    public void onLanding()
    {
        animator.SetBool("isJumping", false);
        jump = false;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
}
