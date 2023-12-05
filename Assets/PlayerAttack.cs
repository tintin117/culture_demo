using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int meleeAttackDamage = 10;
    public float knockbackForce = 0.1f;

    public float pullForce = 10f;
    public float radius = 5f;

    private Animator animator;
    private float pull = 1;

    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;

    private bool canDash = true;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            PerformMeleeAttack();

        }
        if (Input.GetButtonDown("Fire2"))
        {
            PerformSpellSkill();

        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            StartCoroutine(Dash());

        }
    }

    IEnumerator Dash()
    {
        animator.SetTrigger("dash");
        canDash = false;

        // Store the current position of the player
        Vector2 startPosition = transform.position;

        // Calculate the end position of the dash
        print(transform.localScale);

        Vector2 endPosition = startPosition + new Vector2(transform.right.x, transform.right.y) * transform.localScale.x * dashDistance;

        float startTime = Time.time;

        while (Time.time - startTime < dashDuration)
        {
            // Move the player towards the end position
            rb.MovePosition(Vector2.Lerp(startPosition, endPosition, (Time.time - startTime) / dashDuration));

            yield return null;
        }

        canDash = true;

        // Cooldown before the player can dash again
        yield return new WaitForSeconds(dashCooldown);
    }

    void PerformSpellSkill()
    {
        // Play attack animation
        animator.SetTrigger("second_skill");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D enemy_rb = collider.GetComponent<Rigidbody2D>();
            if (enemy_rb != null)
            {
                // Calculate the direction from the enemy to the player
                Vector2 direction = pull*(transform.position - collider.transform.position);

                // Apply force towards the player
                enemy_rb.AddForce(direction.normalized * pullForce, ForceMode2D.Impulse);
            }
        }
        pull *= -1;

    }

    void PerformMeleeAttack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Check for enemies in the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Deal damage to each enemy in the range
        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming enemies have a script with a TakeDamage method
            enemy.GetComponent<Enemy>().TakeDamage(meleeAttackDamage);
            // Apply knockback to the enemy
            Vector2 knockbackDirection = enemy.transform.position - transform.position;
            enemy.GetComponent<Enemy>().ApplyKnockback(knockbackDirection.normalized * knockbackForce);

        }
    }

    // Visualize the attack range in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
