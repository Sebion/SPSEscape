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
    public float movementSpeedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;


    public float jumpPower;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 4f;


    public bool grounded;
    public bool crouching;
    public bool falling;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    public BoxCollider2D groundChecker;
    private Animator anim;
    public GameManager gameManager;


    public AudioSource runningSound;
    public AudioSource fallingSound;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        movementSpeed = gameObject.GetComponent<PlayerEndless>().movementSpeed;
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
        if (!runningSound.isPlaying)
        {
            runningSound.Play();
        }

        if (falling)
        {
            SetBoxCollider2DtoFall();
            groundChecker.offset = new Vector2(0.8255187f, 0.5656123f);
            groundChecker.size = new Vector2(0.603092f, 0.4525841f);
        }
        else if (crouching)
        {
            SetBoxCollider2DtoCrouch();
        }
        else
        {
            groundChecker.offset = new Vector2(0.20877f, 0.31837f);
            groundChecker.size = new Vector2(0.79835f, 0.66052f);
            SetBoxCollider2DtoDefault();
        }


        if (transform.position.x > speedMilestoneCount && movementSpeed <= 16)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= movementSpeedMultiplier;
            movementSpeed *= movementSpeedMultiplier;
        }

        rb2d.velocity = new Vector2(movementSpeed, rb2d.velocity.y);

        //jumping
        if (grounded && CrossPlatformInputManager.GetButton("Jump"))
        {
            rb2d.velocity = (Vector2.up * jumpPower);
            runningSound.Stop();
        }

        //falling
        if (rb2d.velocity.y < 0)
        {
            runningSound.Stop();
            falling = true;


            if (falling && CrossPlatformInputManager.GetButton("Crouch"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        else if (rb2d.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            runningSound.Stop();
        }
        else
        {
            falling = false;
        }

        //crouching
        if (grounded && CrossPlatformInputManager.GetButtonDown("Crouch") ||
            CrossPlatformInputManager.GetButton("Crouch"))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(("KillBox")))
        {
            gameManager.OpenDeathMenu();
            fallingSound.Play();
            runningSound.Stop();
        }
    }

    private void SetBoxCollider2DtoDefault()
    {
        bc2d.offset = new Vector2(0.09898f, -0.0699f);
        bc2d.size = new Vector2(0.75518f, 1.6345f);
    }

    private void SetBoxCollider2DtoCrouch()
    {
        bc2d.offset = new Vector2(0.32734f, -0.3190f);
        bc2d.size = new Vector2(1.21191f, 1.13045f);
    }

    private void SetBoxCollider2DtoFall()
    {
        bc2d.offset = new Vector2(0.3824404f, -0.04443032f);
        bc2d.size = new Vector2(0.6923795f, 1.583561f);
    }
}