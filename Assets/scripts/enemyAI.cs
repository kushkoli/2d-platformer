using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    
    public Transform player;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject healthbar;

    
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackCooldown = 1f;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Distance to player
        float distance = Mathf.Abs(player.position.x - transform.position.x);

        if (!isAttacking)
        {
            // Flip only if not attacking
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            transform.localScale = new Vector3(direction, 1, 1);
        }

        if (isGrounded && distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        else if (isGrounded && distance <= chaseRange && !isAttacking)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
            animator.SetBool("iswalking", true);
            healthbar.SetActive(true);
        }
        else if (isGrounded && !isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("iswalking", false);
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Stop moving
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool("iswalking", false);

        // Trigger attack animation
        animator.SetTrigger("attack");

        // Wait for the attack duration (adjust to match animation length)
        yield return new WaitForSeconds(attackCooldown);

        // ✅ Flip to face player after attack ends
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = new Vector3(direction, 1, 1);

        isAttacking = false;
    }
}
