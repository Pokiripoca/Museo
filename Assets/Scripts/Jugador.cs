using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 8f;   
    public float deceleration = 10f;  

    [Header("Jump")]
    public float jumpForce = 5f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    bool readyToJump;

    public Transform orientation;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 targetVelocity;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        Vector3 targetVelXZ = moveDirection * moveSpeed;

        Vector3 currentVelXZ = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        Vector3 newVelXZ;

        if (moveDirection.magnitude > 0.1f)
        {
            newVelXZ = Vector3.Lerp(currentVelXZ, targetVelXZ, acceleration * Time.fixedDeltaTime);

            if (!grounded)
            {
                newVelXZ = Vector3.Lerp(currentVelXZ, targetVelXZ * airMultiplier, (acceleration * 0.5f) * Time.fixedDeltaTime);
            }
        }
        else
        {
            newVelXZ = Vector3.Lerp(currentVelXZ, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = new Vector3(newVelXZ.x, rb.linearVelocity.y, newVelXZ.z);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}