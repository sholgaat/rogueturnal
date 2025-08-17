using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    private EnemyController enemyController;

    void Awake()
    {
        currentHealth = maxHealth;
        enemyController = GetComponent<EnemyController>();
    }

    public void TakeDamage(int damage, Vector2 attackDirection)
    {
        Debug.Log("Taking damage");
        currentHealth -= damage;

        // Trigger knockback / hit reaction
        if (enemyController != null)
        {
            Debug.Log("Triggering enemy hit");
            enemyController.TakeHit(attackDirection);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
