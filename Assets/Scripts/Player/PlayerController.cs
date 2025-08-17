using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Attack")]
    public MeleeAttack meleeAttack;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private InputSystemActions controls;
    private bool isGrounded = false;
    private bool isDashing = false;
    private float dashTime;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputSystemActions();

        // Movement input
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        // Jump input
        controls.Player.Jump.performed += ctx => Jump();

        // Dash input
        controls.Player.Dash.performed += ctx => StartDash();

        // Attack input
        controls.Player.Attack.performed += ctx =>
        {
            if (meleeAttack != null)
                meleeAttack.TriggerAttack(facingRight);
        };
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        // Animate walking
        animator.SetBool("isWalking", movementInput.x != 0);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Dash timer
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
                isDashing = false;
        }

        // Flip player if moving left/right
        if (movementInput.x > 0 && !facingRight)
            Flip();
        else if (movementInput.x < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.x = movementInput.x * moveSpeed;
            rb.linearVelocity = velocity;
        }
        else
        {
            rb.linearVelocity = new Vector2((facingRight ? 1 : -1) * dashSpeed, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void StartDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
