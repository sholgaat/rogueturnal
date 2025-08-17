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
    public Transform player;
    public float aggroRange = 5f;

    [Header("Hit")]
    public Vector2 hitForce = new Vector2(5f, 2f);

    private Rigidbody2D rb;
    private bool movingRight = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = false; // start moving left
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Flip if reached edge
        if (!isGrounded)
            Flip();

        // Patrol
        if (!PlayerInAggro())
        {
            if (transform.position.x > rightLimit)
                movingRight = false;
            else if (transform.position.x < leftLimit)
                movingRight = true;

            Move();
        }
        else
        {
            // Move towards player
            float dir = (player.position.x > transform.position.x) ? 1 : -1;
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

            // Flip sprite if needed
            if ((dir > 0 && transform.localScale.x < 0) || (dir < 0 && transform.localScale.x > 0))
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.linearVelocity.y);

        // Flip sprite if needed
        if ((movingRight && transform.localScale.x < 0) || (!movingRight && transform.localScale.x > 0))
            Flip();
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool PlayerInAggro()
    {
        if (player == null) return false;
        return Mathf.Abs(player.position.x - transform.position.x) <= aggroRange;
    }

    public void TakeHit(Vector2 attackDirection)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(hitForce.x * attackDirection.x, hitForce.y), ForceMode2D.Impulse);
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
