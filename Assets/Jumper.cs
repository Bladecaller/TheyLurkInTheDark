using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Rigidbody2D body;
    public bool m_FacingRight;
    public int maxHealth;
    public Animator animator;
    public Transform player;
    public int currentHealth;
    public int attackDmg;

    void Start()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
    }
    void Update(){
        if(Vector2.Distance(transform.position, player.position) < 10)
        {
            if(transform.position.x > player.transform.position.x){
                if(m_FacingRight)
                {
                    Flip();
                }
                body.velocity = new Vector2(-1 , 0);
            }else
            {
                if(!m_FacingRight)
                {
                    Flip();
                }
                body.velocity = new Vector2(1 , 0);
            }
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerCombat>().takeDMG(attackDmg);
        }
    }

    public void takeDMG(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log(currentHealth);
        body.velocity = new Vector2(3,0);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
