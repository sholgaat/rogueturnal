using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            // Determine attack direction
            Vector2 attackDir = (other.transform.position.x > transform.position.x) ? Vector2.left : Vector2.right;
            enemy.TakeDamage(damage, attackDir);
        }
    }
}
