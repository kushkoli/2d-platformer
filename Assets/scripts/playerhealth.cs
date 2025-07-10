using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        // Trigger death animation
        animator.SetTrigger("die");

        // Disable player input scripts (excluding this one)
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }

        // Zero horizontal movement but let gravity act
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // stop horizontal momentum
        }

        // Wait until the player touches the ground
        yield return new WaitUntil(() => rb.IsTouchingLayers(LayerMask.GetMask("Ground")));

        yield return new WaitForSeconds(0.5f); // settle time

        // Freeze everything
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Optional: Destroy the player or reload scene
        // Destroy(gameObject);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
