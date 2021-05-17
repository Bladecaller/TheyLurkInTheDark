using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float jumpForce = 400f;
	[Range(0, .3f)] public float movementSmoothing = .05f;
	public LayerMask whatIsGround;
	public Transform groundCheck;

	const float groundedRadius = 0.2f; 
	public bool grounded;            
	private Rigidbody2D rigidbody2D;
	private bool facingRight = true;
	private Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
			}
		}
	}


	public void Move(float move, bool jump)
	{
		Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
		rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

		if (move > 0 && !facingRight)
		{
			Flip();
		}else if (move < 0 && facingRight)
		{
			Flip();
		}

		if (grounded && jump)
		{
			grounded = false;
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
		}
	}


	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
