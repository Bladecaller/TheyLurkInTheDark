using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator animator;
    public Rigidbody2D Rigidbody2D;
    bool jump = false;

    bool midJump;

    float jumpTimeCounter;

    public float jumpTime;

    float horisontalMove = 0;

    public float runSpeed = 40f;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetButtonDown("Sprint"))
        {
            animator.SetBool("sprint", true);
            runSpeed *=1.4f;
        }else if(Input.GetButtonUp("Sprint"))
        {
            animator.SetBool("sprint", false);
            runSpeed /=1.4f;
        }

        horisontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("velocity", Mathf.Abs(horisontalMove));
    
        if(Input.GetKeyDown(KeyCode.Space) && controller.grounded)
        {
            midJump = true;
            jumpTimeCounter = jumpTime;
            jump = true;
            animator.SetBool("jump", true);
        }

        if(Input.GetKey(KeyCode.Space) && midJump)
        {
            if(jumpTimeCounter > 0)
            {
                Rigidbody2D.velocity = Vector2.up * controller.jumpForce; 
                horisontalMove *= controller.jumpForce/3;
                jumpTimeCounter -= Time.deltaTime;
            }else
            {
                midJump = false;
            }
            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            midJump = false;
        }

        animator.SetBool("grounded", controller.grounded);
    }

    void FixedUpdate()
    {
        controller.Move(horisontalMove * Time.fixedDeltaTime, jump);

        jump = false;
    }
    void PlaySound(string sound){
        FindObjectOfType<AudioManager>().Play(sound);
    }
    void StopSound(string sound){
        FindObjectOfType<AudioManager>().Stop(sound);
    }
}