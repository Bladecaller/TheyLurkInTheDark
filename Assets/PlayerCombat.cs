using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public GameObject hpBar;
    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayers;
    public int attackDmg;
    public int maxHealth = 100;
    public int currentHealth;
    private bool airAttack = false;
    private bool dead = false;

       void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(Input.GetButtonDown("attack1"))
        {
            if(GetComponent<CharacterController2D>().grounded && !dead)
            {
                Attack1();

            }else if(!dead)
            {
                AttackAir();
            }  
        }

        hpBar.GetComponent<HealthBar>().SetHP(currentHealth);

        if(GetComponent<CharacterController2D>().grounded)
        {
            animator.SetBool("midair attack", false);  
        }
    }

    void Attack1()
    {
        animator.SetTrigger("attack");
        FindObjectOfType<AudioManager>().Play("SwordSlash");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDMG(attackDmg);
            FindObjectOfType<AudioManager>().Play("SwordHit");
        }
    }

    void AttackAir()
    {
        animator.SetBool("midair attack", true);
        animator.SetTrigger("attack air");
        FindObjectOfType<AudioManager>().Play("SwordSlash");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDMG(attackDmg);
            FindObjectOfType<AudioManager>().Play("SwordHit");
        }
    }

    public void takeDMG(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            if(!dead)
            {
                dead = true;
                Die();
            }
        }
    }

    void Die()
    {
        animator.SetTrigger("dead");
        FindObjectOfType<AudioManager>().Play("Death");
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
