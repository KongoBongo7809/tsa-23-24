using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public ExperienceCollector playerExperience;

    public int maxHealth = 100;
    public int currentHealth;

    //Attack stats
    public Transform attackPoint;
    public LayerMask playerLayer;

    public int attackDamage = 15;
    public float attackRange = 0.5f;

    public float attackRate = 1f;
    float nextAttackTime;

    public int onDeathExperience = 5;
    bool playerInRangeLastFrame = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (playerInRange())
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        //Player in range audio
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (playerInRange() && !playerInRangeLastFrame)
        {
            audioManager.Play("Enemy Growl");
        }
        playerInRangeLastFrame = playerInRange();
    }

    bool playerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("Hurt");
        FindObjectOfType<AudioManager>().Play("Enemy Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        //Attack animation
        animator.SetTrigger("Attack");

        //Damage player
        foreach (Collider2D player in hitPlayer)
        {
            if (!player.GetComponent<PlayerCombat>().animator.GetBool("isDead"))
            {
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
        }
    }

    void Die()
    {
        //Debug.Log("Enemy Dead");

        animator.SetBool("isDead", true);

        Destroy(this.gameObject);
        playerExperience.experience += onDeathExperience;
        playerExperience.experienceText.text = "" + playerExperience.experience;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
