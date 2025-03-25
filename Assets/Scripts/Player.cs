using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private void Update()
    {
        // get movement input
        horizontal = Input.GetAxisRaw("Horizontal");

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private void FixedUpdate()
    {
        // choose speed based on run input
        float speed = Input.GetButton("Run") ? runSpeed : walkSpeed;

        // movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // detecting if player is on ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
    }
}
