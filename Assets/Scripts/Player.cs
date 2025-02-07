using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float moveSpeed = 3f;
    Vector3 moveDirection;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.8f;
    float yVelocity;
    float inAirTimer;

    bool isGrounded = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckGrounded();

        ReadMovementInputs();
        ReadJumpInput();

        HandleGrounded();
        HandleAirborne();

        ApplyMovement();
        ApplyGravity();
    }

    private void ReadMovementInputs()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void ReadJumpInput()
    {
        if (!isGrounded) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            yVelocity = Mathf.Sqrt(-2.0f * gravity * jumpHeight);
            inAirTimer = 0.01f;
        }
    }

    private void CheckGrounded()
    {
        if (yVelocity > 0f)
        {
            isGrounded = false;
            return;
        }

        //IsGrounded is always false for the first 0.1 seconds in air
        if (inAirTimer > 0f && inAirTimer < 0.1f)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics.CheckSphere(transform.position + (0.25f * characterController.radius) * Vector3.up, characterController.radius, LayerMask.GetMask("Ground"));
    }

    private void ApplyGravity()
    {
        characterController.Move(yVelocity * Vector3.up * Time.deltaTime);
    }

    private void HandleGrounded()
    {
        if (!isGrounded) return;

        inAirTimer = 0;
        yVelocity = 0;
    }

    private void HandleAirborne()
    {
        if (isGrounded) return;

        inAirTimer += Time.deltaTime;
        yVelocity += gravity * Time.deltaTime;
    }

    private void ApplyMovement()
    {
        float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Vector3 targetMoveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;

        if(moveDirection.magnitude > 0)
        {
            characterController.Move(targetMoveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
