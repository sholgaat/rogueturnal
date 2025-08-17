using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackHitbox;   // The hitbox object with collider
    public int damageAmount = 1;      // How much damage this attack deals
    public float attackDuration = 0.2f; // How long the hitbox stays active

    void Start()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false); // Make sure the hitbox is hidden initially
    }

    public void TriggerAttack(bool facingRight)
    {
        if (attackHitbox == null) return;

        // Flip hitbox based on player facing
        Vector3 scale = attackHitbox.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1);
        attackHitbox.transform.localScale = scale;

        // Activate hitbox
        attackHitbox.SetActive(true);

        // Automatically deactivate after attackDuration
        StartCoroutine(DisableHitboxAfterDelay());
    }

    private IEnumerator DisableHitboxAfterDelay()
    {
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.SetActive(false);
    }

    // This should be on the hitbox GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            // Determine attack direction based on hitbox scale (matches player facing)
            Vector2 attackDirection = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);

            // Apply damage with direction
            enemy.TakeDamage(damageAmount, attackDirection);
        }
    }
}
