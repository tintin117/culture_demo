using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldSkill : MonoBehaviour
{
    public float pushForce = 10f;
    public float radius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Assuming 'F' is the key to activate the skill
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Rigidbody2D enemyRigidbody = collider.GetComponent<Rigidbody2D>();

                    if (enemyRigidbody != null)
                    {
                        Vector2 direction = (enemyRigidbody.position - (Vector2)transform.position).normalized;
                        enemyRigidbody.AddForce(direction * pushForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

