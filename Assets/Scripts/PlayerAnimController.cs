﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public AudioSource jumpsfx,walksfx,grabsfx,putsfx;
    public float moveSpeed;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
   // private bool grounded;
    private bool doubleJump;
    private bool faceRight, isJumping;
    //to grab
    private bool isGrabbing;
    float speed;
    //Makes a list of triggers
   // List<Collider2D> collidors;

    Animator anim;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        //collidors = new List<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        faceRight = true;
    }

    private void FixedUpdate()
    {
     //   grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    
    void Update()
    {
        MovePlayer(speed);

        Flip();
        if (Input.GetKeyDown(KeyCode.A))
        {
            speed = -moveSpeed;
            walksfx.Play();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            speed = 0;
            walksfx.Stop();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            speed = moveSpeed;
            walksfx.Play();
         
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            speed = 0;
            walksfx.Stop();
      
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isGrabbing = !isGrabbing;
        }

        if (isGrabbing)
        {
            anim.SetInteger("State", 4);
            grabsfx.Play();
        }

        /*
        if (Input.GetKeyUp(KeyCode.E))
        {
            isGrabbing = !isGrabbing;
            if (isGrabbing)
            {
                anim.SetInteger("State", 4);
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight));
            // grounded = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            isJumping = true;
            anim.SetInteger("State", 1);
            jumpsfx.Play();


        }
   
        /*
        if (grounded)
        {
            doubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !doubleJump && !grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doubleJump = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetButtonDown("e"))
        {
            collidors.ForEach(n => n.SendMessage("Use", SendMessageOptions.DontRequireReceiver));
        }
    */
    }
    public void MovePlayer(float playerSpeed)
    {
        if ((playerSpeed < 0 && (!isJumping) && (isGrabbing)) || (playerSpeed > 0 && (!isJumping) && (isGrabbing)))
        {
            anim.SetInteger("State", 4);
            if (playerSpeed > 0)
                faceRight = true;
            if (playerSpeed < 0)
                faceRight = false;
        }
        if ((playerSpeed < 0 && (!isJumping) && (!isGrabbing)) || (playerSpeed > 0 && (!isJumping) && (!isGrabbing)))
        {
            anim.SetInteger("State", 2);
        }
        if((playerSpeed == 0) && (!isJumping))
        {
            anim.SetInteger("State", 3);
        }
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }
    
    void Flip()
    {
        if(((speed < 0) && (faceRight)) || ((speed > 0) && (!faceRight)))
        {
            faceRight = !faceRight;
            //Vector3 temp = transform.localScale;
            //temp.x *= -1;
            //transform.localScale = temp;
            if (faceRight)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platforms")
        {
            isJumping = false;
            anim.SetInteger("State", 3);
         
        }
    }
    /*  private void OnTriggerEnter2D(Collider2D collision)
      {
          collidors.Add(collision);
      }

      private void OnTriggerExit2D(Collider2D collision)
      {
          collidors.Remove(collision);
      }
      */
   
      
}
