using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public Image healthbar;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyAI enemyAI = GetComponent<EnemyAI>();
    }
    

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0f, 1f);
        
    }

    private System.Collections.IEnumerator Die()
    {
        isDead = true;

        // ❌ Stop movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic; // disables physics
        }

        // ❌ Stop other scripts like enemy chase
        MonoBehaviour[] allScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script != this)
                script.enabled = false;
        }

        // ✅ Play death animation
        animator.SetTrigger("die");

        // ✅ Wait for the animation to finish
        yield return new WaitForSeconds(1f); // adjust to your animation length

        Destroy(gameObject);
    }
}
