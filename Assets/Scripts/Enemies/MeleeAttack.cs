using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public GameObject attackHitbox;      // Assign the hitbox child in Inspector
    public float attackDuration = 0.2f;  // how long the hitbox stays active

    void Start()
    {
        attackHitbox.SetActive(false); // hide hitbox initially
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
}
