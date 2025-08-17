using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public GameObject attackHitbox;  // assign the hitbox GameObject
    public float attackDuration = 0.2f;

    void Start()
    {
        attackHitbox.SetActive(false); // hide initially
    }

    public void TriggerAttack(bool facingRight)
    {
        if (attackHitbox == null) return;

        // Flip hitbox
        Vector3 scale = attackHitbox.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1);
        attackHitbox.transform.localScale = scale;

        // Activate hitbox
        attackHitbox.SetActive(true);
        StartCoroutine(DisableHitboxAfterDelay());
    }

    IEnumerator DisableHitboxAfterDelay()
    {
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.SetActive(false);
    }
}
