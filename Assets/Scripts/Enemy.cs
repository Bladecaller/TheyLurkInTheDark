using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public bool facingRight;
    public Animator animator;
    public Transform player;
    private bool grounded = true;
    public bool dead = false;
    private int currentHealth;
    private int maxHealth;
    private int speed;
    private int attackDmg;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if(this.tag == "Crab"){
            maxHealth = 100;
            attackDmg = 5;
            speed = 1;
            currentHealth = maxHealth;
        }

        if(this.tag == "Octopus"){
            maxHealth = 200;
            attackDmg = 10;
            speed = 2;
            currentHealth = maxHealth;
        }

        if(this.tag == "Jumper"){
            maxHealth = 250;
            attackDmg = 15;
            speed = 3;
            currentHealth = maxHealth;
        }
    }

    void Update()
    {   
        if(Vector2.Distance(transform.position, player.position) < 10 && Vector2.Distance(transform.position, player.position) > 1)
        {
            if(this.tag == "Jumper" && grounded)
            {
                animator.SetTrigger("jump");
                body.AddForce(new Vector2(0, 1000));
                grounded = false;
            }
            

            if(transform.position.x > player.transform.position.x)
            {
                if(facingRight)
                {
                    Flip();
                }

            Vector2 velocity = body.velocity;
		    velocity.x = -speed;
		    body.velocity = velocity;

            }else
            {
                if(!facingRight)
                {
                    Flip();
                }

            Vector2 velocity = body.velocity;
		    velocity.x = speed;
		    body.velocity = velocity;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("MonsterDMG");
            other.gameObject.GetComponent<PlayerCombat>().takeDMG(attackDmg);
        }
    }

    public virtual void takeDMG(int dmg)
    {
        currentHealth -= dmg;
        
        if(facingRight)
        {   
            Vector2 vel = body.velocity;
            body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(-4000, 300));
            body.velocity = vel;
        }else
        {
            Vector2 vel = body.velocity;
            body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(4000, 300));
            body.velocity = vel; 
        }


        if(currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("MonsterDeath");
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        dead = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        //this.enabled = false;
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public bool GetDeath()
    {
        return dead;
    }
}
