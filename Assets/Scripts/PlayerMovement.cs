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
    public float jumpForce = 3.0f;
    public float zRotationLimit = 10.0f; // How much wobble
    public float idleAnimCoyoteTime = 0.15f; // The grace period duration
    public float idleAnimCoyoteTimeCounter;
    public float input;
    public bool isGrounded;





    // Start is called before the first frame update
    void Start()
    {
        // set initial stuff to avoid doing it manually in the editor
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb.gravityScale = playerGravity;
        groundLayer = LayerMask.GetMask("Ground");

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
            playerRb.linearVelocity = Vector2.up * jumpForce;
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