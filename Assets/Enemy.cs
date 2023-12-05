using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("being_hit");
        print(damage);
        // Subtract the damage from the current health
        currentHealth -= damage;

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to apply knockback to the enemy
    public void ApplyKnockback(Vector2 knockbackDirection)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Optional: Reset the current velocity
            rb.AddForce(knockbackDirection, ForceMode2D.Impulse);
        }
    }

    // Function to handle enemy defeat
    private void Die()
    {
        // Perform any death-related actions (e.g., play death animation, drop items, etc.)
        // For simplicity, we'll destroy the enemy GameObject in this example
        Destroy(gameObject);
    }
}
