using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has EnemyHealth
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Calculate attack direction
            Vector2 attackDirection = (collision.transform.position - transform.parent.position).normalized;
            enemyHealth.TakeDamage(damage, attackDirection);
        }
    }
}
