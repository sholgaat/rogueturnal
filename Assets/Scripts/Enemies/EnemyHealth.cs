using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private EnemyController enemyController;
    private Animator animator;

    void Awake()
    {
        currentHealth = maxHealth;
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Call this to apply damage to the enemy.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    /// <param name="attackDirection">Direction of the attack: Vector2.right for right, Vector2.left for left</param>
    public void TakeDamage(int damage, Vector2 attackDirection)
    {
        currentHealth -= damage;

        // Play hit animation if exists
        if (animator != null)
            animator.SetTrigger("Hit");

        // Apply knockback via EnemyController
        if (enemyController != null)
            enemyController.TakeHit(attackDirection);

        // Check if enemy is dead
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Optional: play death animation
        if (animator != null)
            animator.SetTrigger("Die");

        // Disable enemy behavior
        if (enemyController != null)
            enemyController.enabled = false;

        // Disable colliders so player can pass through
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
            col.enabled = false;

        // Destroy game object after short delay to let animation play
        Destroy(gameObject, 0.5f);
    }
}
