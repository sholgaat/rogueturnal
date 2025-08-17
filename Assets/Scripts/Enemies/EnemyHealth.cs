using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public RectTransform healthFill; // assign Fill from Canvas

    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount, Vector2 attackDirection)
    {
        currentHealth -= amount;

        // Apply knockback
        EnemyController controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.TakeHit(attackDirection);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            float ratio = (float)currentHealth / maxHealth;
            Vector3 scale = healthFill.localScale;
            scale.x = Mathf.Clamp01(ratio);
            healthFill.localScale = scale;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
