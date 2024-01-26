using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    //Attack stats
    public Transform attackPoint;
    public LayerMask playerLayer;

    public int attackDamage = 15;
    public float attackRange = 0.5f;

    public float attackRate = 1f;
    float nextAttackTime;

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

        if(currentHealth <= 0)
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
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    void Die()
    {
        //Debug.Log("Enemy Dead");

        animator.SetBool("isDead", true);

        Destroy(this.gameObject);
    }
}
