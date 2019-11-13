using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerEndless : MonoBehaviour
{
    public float movementSpeed;


    public float jumpPower;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 4f;
    public float maxTime;

    public bool grounded;
    public bool crouching;
    public bool falling;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Animator anim;
    private float screenWidth;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        movementSpeed = gameObject.GetComponent<PlayerEndless>().movementSpeed;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        //setting animation parameters
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Math.Abs(rb2d.velocity.x));
        anim.SetBool("Crouching", crouching);
        anim.SetBool("Falling", falling);


       
    }


    private void FixedUpdate()
    {
        //horizontal movement
         rb2d.velocity = new Vector2(movementSpeed,rb2d.velocity.y);
        //jumping
        if (grounded && CrossPlatformInputManager.GetButton("Jump"))
        {
            rb2d.velocity = (Vector2.up * jumpPower);
        }

        //falling
        if (rb2d.velocity.y < 0)
        {
            falling = true;
            if (falling && CrossPlatformInputManager.GetButton("Crouch"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        else if (rb2d.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else falling = false;


        //crouching
        if (grounded && CrossPlatformInputManager.GetButtonDown("Crouch") ||
            CrossPlatformInputManager.GetButton("Crouch"))
        {
            crouching = true;
            bc2d.offset = new Vector2(0.09703f, -0.5678f);
            bc2d.size = new Vector2(0.9958f, 0.8754f);
        }
        else
        {
            crouching = false;
            bc2d.offset = new Vector2(-0.03028131f, -0.3534986f);
            bc2d.size = new Vector2(0.8758283f, 1.294194f);
        }
    }
}