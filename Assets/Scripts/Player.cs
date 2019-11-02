using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float crouchingSpeed;
    private float actualSpeed;
    private bool facingRight = true;

    public float jumpPower;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 4f;

    public bool grounded;
    public bool crouching;
    public bool falling;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Animator anim;
    public Joystick joystick;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        movementSpeed = gameObject.GetComponent<Player>().movementSpeed;
        actualSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //setting animation parameters
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Math.Abs(rb2d.velocity.x));
        anim.SetBool("Crouching", crouching);
        anim.SetBool("Falling", falling);

        //rotating character

        if (joystick.Horizontal >= 0.01 && !facingRight)
        {
            flipCharacter();
        }

        else if (joystick.Horizontal <= -0.01 && facingRight)
        {
            flipCharacter();
        }
    }

    private void FixedUpdate()
    {
        //horizontal movement
        if (joystick.Horizontal>0.3f)
        {
            rb2d.velocity = new Vector2(actualSpeed,rb2d.velocity.y);
        }else if (joystick.Horizontal< -0.3f)
        {
            rb2d.velocity = new Vector2(-actualSpeed,rb2d.velocity.y);
        }else rb2d.velocity = new Vector2(0,rb2d.velocity.y);
        

        //jumping
        if (grounded && joystick.Vertical > 0.5f)
             {
                 rb2d.velocity = (Vector2.up * jumpPower);
             }
     
             //falling
             if (rb2d.velocity.y < 0)
             {
                 falling = true;
                 if (falling && joystick.Vertical < -0.5f)
                 {
                     rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                 }
             }
     
             else if (rb2d.velocity.y > 0 && joystick.Vertical <0.5f)
             {
                 rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
             }
             else falling = false;
     
     
             //crouching
             if (grounded && joystick.Vertical < -0.5f )
             {
                 crouching = true;
                 bc2d.offset = new Vector2(0.09703f, -0.5678f);
                 bc2d.size = new Vector2(0.9958f, 0.8754f);
     
                 actualSpeed = crouchingSpeed;
             }
             else
             {
                 crouching = false;
                 bc2d.offset = new Vector2(-0.03028131f, -0.3534986f);
                 bc2d.size = new Vector2(0.8758283f, 1.294194f);
     
                 actualSpeed = movementSpeed;
             }
         }

    private void flipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}