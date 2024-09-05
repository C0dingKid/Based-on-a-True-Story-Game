using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidPlayer;
    [SerializeField] private Transform playerSprite; // Reference to the sprite, not the whole player object
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jump;
    [SerializeField] private float runSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] public float jumpCooldown;
    [SerializeField] public Animator walkingAnimation;
    [SerializeField] private AudioClip[] audioClips; // Array of audio clips


    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component
    private int currentTrackIndex = 0;
    private bool switchTrack = false;


    public float minScale = 0.2f;
    public float shrinkAmount = 0.9f;


    public FindMushroom findMushroom;
    public FindMushroom2 findMushroom2;
    public FindMushroom3 findMushroom3;


    private bool isFacingRight;
    public bool isGrounded;
    public bool readyToJump;
    private float currentSpeed;
    private bool isGravityReversed = false; // New variable to track gravity state


    public float size;
    public float shrinkStep = 0.05f;  // Set a small decrement for shrinking


    private Vector2 originalGroundCheckPosition;
    private Vector3 defaultScale;  // Store the default scale
    private float defaultGravity;  // Store the default gravity scale
    private bool hasEaten1;
    private bool hasEaten2;


    private bool hasEaten3;


    private void Start()
    {
        hasEaten1 = false;
        hasEaten2 = false;
        hasEaten3 = false;
        readyToJump = true;
        isFacingRight = true;
        originalGroundCheckPosition = groundCheck.localPosition; // Store the original ground check position


        // Initialize default values
        defaultScale = transform.localScale;
        defaultGravity = rigidPlayer.gravityScale;


        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentTrackIndex];
            audioSource.Play();
            StartCoroutine(LoopTrack());
        }
    }


    private IEnumerator LoopTrack()
    {
        while (true)
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }



            if (switchTrack)
            {
                currentTrackIndex += 1;
                audioSource.clip = audioClips[currentTrackIndex];
                switchTrack = false;
                findMushroom.isEating = false;
                findMushroom2.isEating = false;
                findMushroom3.isEating = false;
            }


            audioSource.Play();
        }
    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        size = transform.localScale.x;
        MyInput();


        if (rigidPlayer.velocity.x != 0)
        {
            walkingAnimation.SetBool("isWalking", true);
        }
        else
        {
            walkingAnimation.SetBool("isWalking", false);
        }


        if (rigidPlayer.velocity.y != 0)
        {
            walkingAnimation.SetBool("isJumping", true);
        }
        else
        {
            walkingAnimation.SetBool("isJumping", false);
        }


        if ((findMushroom.isEating && !hasEaten1) || (findMushroom2.isEating && !hasEaten2) || (findMushroom3.isEating && !hasEaten3))
        {
            walkingAnimation.Play("eat", 0, 0f);
            if (findMushroom.isEating) { hasEaten1 = true; }
            if (findMushroom2.isEating) { hasEaten2 = true; }
            if (findMushroom3.isEating) { hasEaten3 = true; }
        }


        // Check if Mushroom3 is eaten and reset player size if needed
        if (findMushroom3.ateMush3)
        {
            ResetSizeAndPhysics();
            findMushroom3.ateMush3 = false;  // Reset the flag after handling


        }


        if (findMushroom.isEating || findMushroom2.isEating || findMushroom3.isEating)
        {
            switchTrack = true;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    private void MyInput()
    {
        float horizontalInput = 0f;
        float currentSpeed = moveSpeed;


        if (Input.GetKey(KeyCode.A))
        {
            if (isFacingRight)
            {
                Flip();
            }
            horizontalInput = -1f;
        }


        if (Input.GetKey(KeyCode.D))
        {
            if (!isFacingRight)
            {
                Flip();
            }
            horizontalInput = 1f;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= runSpeed;
        }


        rigidPlayer.velocity = new Vector2(horizontalInput * currentSpeed, rigidPlayer.velocity.y);


        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && isGrounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }


    private void Jump()
    {
        if (findMushroom.canReverse)
        {
            isGravityReversed = !isGravityReversed;
            rigidPlayer.gravityScale *= -1;


            // Flip the player vertically by adjusting the y scale, based on gravity state
            Vector3 scale = playerSprite.localScale;
            scale.y = Mathf.Abs(scale.y) * (isGravityReversed ? -1 : 1);  // Flip y-axis based on gravity
            playerSprite.localScale = scale;


            // Move the ground check to the new position when gravity is reversed
            groundCheck.localPosition = isGravityReversed ? new Vector2(0.242022f, 0.6f) : originalGroundCheckPosition;
        }


        if (findMushroom2.canShrink)
        {
            // Gradually decrease the player's size by a small, fixed amount on each jump
            transform.localScale = new Vector3(
                Mathf.Max(transform.localScale.x - shrinkStep, minScale),
                Mathf.Max(transform.localScale.y - shrinkStep, minScale),
                transform.localScale.z
            );


            // Ensure the scale doesn't go below the minScale
            if (transform.localScale.x < minScale || transform.localScale.y < minScale)
            {
                transform.localScale = new Vector3(minScale, minScale, transform.localScale.z);
            }
        }


        // Reset vertical velocity before jumping
        rigidPlayer.velocity = new Vector2(rigidPlayer.velocity.x, 0);


        // Apply jump force in the appropriate direction based on gravity state
        rigidPlayer.AddForce(new Vector2(0, isGravityReversed ? -jump : jump), ForceMode2D.Impulse);
    }


    private void ResetJump()
    {
        readyToJump = true;
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;


        // Flip the sprite by negating the x scale, without affecting the physics
        Vector3 scale = playerSprite.localScale;
        scale.x *= -1;
        playerSprite.localScale = scale;
    }


    private void ResetSizeAndPhysics()
    {
        // Reset scale and gravity to default values
        transform.localScale = defaultScale;
        rigidPlayer.gravityScale = defaultGravity;


        // Reset gravity and vertical scale flipping
        isGravityReversed = false;
        Vector3 scale = playerSprite.localScale;
        scale.y = Mathf.Abs(scale.y);  // Ensure y scale is positive
        playerSprite.localScale = scale;


        // Reset ground check position
        groundCheck.localPosition = originalGroundCheckPosition;
    }


}










