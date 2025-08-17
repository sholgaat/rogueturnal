using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float leftLimit = 2f;
    public float rightLimit = 2f;

    [Header("Aggro")]
    public Transform player;          // assign the player in inspector
    public float aggroRange = 5f;     // distance at which enemy starts chasing
    public float chaseBuffer = 1f;    // extra distance to prevent flicker

    [Header("Hit Reaction")]
    public Vector2 hitForce = new Vector2(5f, 2f);
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private bool movingRight = false;
    private bool chasingPlayer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = false; // Start moving left
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (!isGrounded)
            Flip();

        if (player != null)
        {
            float distanceToPlayer = player.position.x - transform.position.x;

            if (Mathf.Abs(distanceToPlayer) <= aggroRange)
            {
                chasingPlayer = true;
                movingRight = distanceToPlayer > 0;
            }
            else if (chasingPlayer && Mathf.Abs(distanceToPlayer) > aggroRange + chaseBuffer)
            {
                // Player left range; resume patrol
                chasingPlayer = false;
                // Determine direction based on patrol limits
                movingRight = transform.position.x < (leftLimit + rightLimit) / 2f;
            }
        }
        else
        {
            chasingPlayer = false;
        }

        // Patrol if not chasing
        if (!chasingPlayer)
        {
            if (transform.position.x > rightLimit)
                movingRight = false;
            else if (transform.position.x < leftLimit)
                movingRight = true;
        }

        Move();
    }

    void Move()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = (movingRight ? 1 : -1) * moveSpeed;
        rb.linearVelocity = velocity;

        // Flip sprite if needed
        if ((movingRight && transform.localScale.x < 0) || (!movingRight && transform.localScale.x > 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeHit(Vector2 attackDirection)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(hitForce.x * attackDirection.x, hitForce.y), ForceMode2D.Impulse);
        if (animator != null)
            animator.SetTrigger("Hit");
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}
