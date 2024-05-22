using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public BoxCollider2D hitbox;
    public Animator animator;

    [Header("Attack")]
    public Transform attackPoint;
    public LayerMask enemyLayers;
    
    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime;

    [Header("Health")]
    public int maxHealth = 100;
    public float healthPs = 2.5f;
    float currentHealth;

    public HealthBar healthBar;

    [Header("Death")]
    public Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //Temporary attack
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(-20);
        }*/

        currentHealth += healthPs * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthBar.SetHealth((int)currentHealth);

    }

    void Attack()
    {
        //Play an attack animation and attack audio
        animator.SetTrigger("Attack");
        FindObjectOfType<AudioManager>().Play("Sword Clash");

        //Detect enemies in range of an attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyCombat>() != null)
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
            } else
            {
                enemy.GetComponent<BossCombat>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("Player Hurt");
        if ((int)currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("Player Hurt");
        if ((int)currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        Debug.Log("Player Died");

        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
