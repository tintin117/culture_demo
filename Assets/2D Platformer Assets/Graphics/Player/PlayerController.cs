using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canMove = true;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        if (Input.GetButton("Fire1"))
        {
            

        }
        else if (canMove)
        {
            Vector2 movement = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
            animator.SetFloat("move_speed", Mathf.Abs(rb.velocity.x));
        }
        if (rb.velocity.x < -0.1 && facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        };



        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    IEnumerator DelayMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, ~LayerMask.GetMask("Player"));
        return hit.collider != null;
    }
    public void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}

