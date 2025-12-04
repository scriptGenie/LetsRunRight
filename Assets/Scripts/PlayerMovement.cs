using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRb;
    public SpriteRenderer spriteRenderer;
    public Transform feetPosition;
    public LayerMask groundLayer;
    public float groundCheckCircle = 0.1f;
    public float speed = 5.0f;
    public float playerGravity = 3.0f; 
    public float jumpForce = 11.0f;
    public float extendedJumpForce;
    public float extendedJumpGravity;
    public float zRotationLimit = 4.0f; // How much wobble
    public float idleAnimCoyoteTime = 0.15f; // The grace period duration
    public float idleAnimCoyoteTimeCounter;
    public float input;
    public bool isGrounded;
    public bool isJumping;
    // public float jumpTime = 0.35f;
    public float jumpTime = 0.16f;
    public float jumpTimeCounter;



private void ResetPlayerGravity()
    {
        playerRb.gravityScale = playerGravity;
    }


    // Start is called before the first frame update
    void Start()
    {
        // set initial stuff to avoid doing it manually in the editor
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPlayerGravity();
        // playerRb.gravityScale = playerGravity;
        groundLayer = LayerMask.GetMask("Ground");
        extendedJumpForce = jumpForce * 0.7f;
        extendedJumpGravity = 2.0f;

        // find childObject for Feet
        Transform intialFeetPosition = transform.Find("FeetPosition");
        
        if (intialFeetPosition != null)
        {
            feetPosition = intialFeetPosition;
        } else
        {
            Debug.Log("--- child not found 'feetPosition' ---");
        }
    }  


    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        
        if (input < 0)
        {
            spriteRenderer.flipX = false;
        } else if (input > 0)
        {
            spriteRenderer.flipX = true;
        }


        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // set initial jump state
            isJumping = true;
            // reset jumpTimeCounter
            jumpTimeCounter = jumpTime;
            // perform jump
            playerRb.linearVelocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping)
        {

            if (jumpTimeCounter > 0)
            {
                // begin decreasing counter
                jumpTimeCounter -= Time.deltaTime;
                // continue jumping
                playerRb.gravityScale = extendedJumpGravity;
                playerRb.linearVelocity = Vector2.up * extendedJumpForce;
            } else
            {
                // reset jump state
                isJumping = false;
                ResetPlayerGravity();
            }
        }


        if (Input.GetButtonUp("Jump"))
        {
            // reset jump state on button release
            isJumping = false;   
        }
        

    }


    // FixedUpdate is set to run at 50fps
    void FixedUpdate() {

        // Get the current local rotation
        Quaternion currentRotation = transform.localRotation;

        // Convert to Euler angles to access individual axis rotations
        Vector3 eulerRotation = currentRotation.eulerAngles;

        // Clamp the Z-axis rotation between -zRotationLimit and zRotationLimit
        // Note: eulerAngles returns values from 0 to 360, so you might need to adjust for negative limits
        // A common way to handle this is to normalize the angle to -180 to 180 first.
        float clampedZ = Mathf.Clamp(NormalizeAngle(eulerRotation.z), -zRotationLimit, zRotationLimit);

        // Apply the new rotation, keeping X and Y as they were
        transform.localRotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, clampedZ);
    
        // get speeds for movement left/right
        playerRb.linearVelocity = new Vector2 (input * speed, playerRb.linearVelocity.y);


        // if player is moving
        if (playerRb.linearVelocity.x != 0)
        {
            // Grace period
            idleAnimCoyoteTimeCounter = idleAnimCoyoteTime; // Reset counter
        } else
        {
            // stop subtracting if already stopped at 0
            if (idleAnimCoyoteTimeCounter > 0)
            {
               idleAnimCoyoteTimeCounter -= Time.deltaTime; 
            }
        }


        if (idleAnimCoyoteTimeCounter < 0.1f)
        {
            transform.localRotation = Quaternion.identity;
        }

    }

    // Helper function to normalize angles to -180 to 180 range
    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }

}