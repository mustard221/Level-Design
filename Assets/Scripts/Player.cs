using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Collider2D mouseCollider;

    private float horizontal;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float jumpingPower = 16f;
    public float crouchSpeed = 1.5f;
    public Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Image bar;
    [SerializeField] private GameObject attackHitbox;

    [SerializeField] private Collider2D objectToDisableWhenCrouching;

    private bool isGrounded;
    private bool isCrouching = false;
    private bool canMove = false;
    private bool takeDamage = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            canMove = false;
            StartCoroutine(EnableMovement());
        }
        else
        {
            canMove = true;
        }

        mouseCollider = GameObject.FindGameObjectWithTag("Mouse").GetComponent<Collider2D>();

    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(2.5f);
        canMove = true;
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("IsAttack", false);
        attackHitbox.SetActive(false);
    }

    private void Update()
    {
        if (takeDamage)
        {
            bar.fillAmount -= 0.005f;
            takeDamage = false;
        }

        if (!canMove) return;

        // get movement input
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsRunning", Input.GetButton("Run"));

        // Flip sprite based on movement direction
        if (horizontal != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (horizontal > 0 ? 1 : -1);
            transform.localScale = scale;
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButton("Crouch"))
        {
            ToggleCrouch(true);
        }
        else if (!Input.GetButton("Crouch") && isCrouching)
        {
            ToggleCrouch(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetBool("IsAttack", true);
            attackHitbox.SetActive(true);
            StartCoroutine(ResetAttack());
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        // choose speed based on run input
        float speed = Input.GetButton("Run") ? runSpeed : walkSpeed;

        if (isCrouching)
        {
            speed = crouchSpeed;
        }

        // movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // detecting if player is on ground and if colliding with mouse
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = true;
        animator.SetBool("IsJumping", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
        {
            Debug.Log("collided");
            takeDamage = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            isGrounded = false;
    }

    private void ToggleCrouch(bool crouch)
    {
        if (crouch)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);
        }

        if (objectToDisableWhenCrouching != null)
        {
            objectToDisableWhenCrouching.enabled = !isCrouching;
        }
    }
}
