using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 4f;
    public float jumpHeight = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Animator animator;

    bool isGroundedLastFrame = false;

    // Update is called once per frame
    void Update()
    {
        //Animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Horizontal Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight*0.5f);
        }*/
        Flip();

        //Audio
        //Footsteps
        bool isMoving = (horizontal != 0) ? true : false;
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (isMoving && !audioManager.isPlaying("Footsteps") && isGrounded()) audioManager.Play("Footsteps");
        if (!isMoving || !isGrounded()) audioManager.Stop("Footsteps");

        if(isGrounded() && !isGroundedLastFrame)
        {
            audioManager.Play("Jump Land");
        }
        isGroundedLastFrame = isGrounded();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
