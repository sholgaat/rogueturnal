using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private EnemyController enemyController;

    void Awake()
    {
        currentHealth = maxHealth;
        enemyController = GetComponent<EnemyController>();
    }

    public void TakeDamage(int damage, Vector2 attackDirection)
    {
        currentHealth -= damage;

        // Trigger knockback / hit reaction
        if (enemyController != null)
        {
            enemyController.TakeHit(attackDirection);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
