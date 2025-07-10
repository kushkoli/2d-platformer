using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundcheckpos;
    public LayerMask groundlayer;

    [Header("Settings")]
    public float movespeed = 5f;
    public float jumppower = 8f;
    public Vector2 groundchecksize = new Vector2(0.5f, 0.5f);

    private float horizontalmovement;
    public bool canMove = true;
    public bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalmovement * movespeed, rb.linearVelocity.y);

        animator.SetFloat("yvelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", Mathf.Abs(horizontalmovement));

        if (horizontalmovement > 0)
            transform.localScale = new Vector2(1, 1);
        else if (horizontalmovement < 0)
            transform.localScale = new Vector2(-1, 1);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalmovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isgrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumppower);
            animator.SetTrigger("jump");
        }
        else if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;

            if (isgrounded())
            {
                animator.SetTrigger("attack");
                EndAttack(); // You're calling it here intentionally
            }
            else
            {
                animator.SetTrigger("airAttack");
                EndAttack(); // And here
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public bool isgrounded()
    {
        return Physics2D.OverlapBox(groundcheckpos.position, groundchecksize, 0, groundlayer);
    }
}
