using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject attackHitbox; // assign the child hitbox here
    public float attackDuration = 0.2f; // how long hitbox stays active
    public int attackDamage = 1; // damage dealt to enemies

    void Start()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false); // hide initially
    }

    public void TriggerAttack(bool facingRight)
    {
        if (attackHitbox == null) return;

        // Flip hitbox if necessary
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
