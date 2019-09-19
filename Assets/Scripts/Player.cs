using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    public float jumpPower = 500f;
    public bool grounded = true;
    public bool crouching;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Math.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("Crouching",crouching);

        if (Input.GetAxis("Horizontal") >= 0.1)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        if (Input.GetAxis("Horizontal") <= -0.1)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            
        }
        
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        // rb2d.AddForce((Vector2.right * speed) * h);
        rb2d.velocity = new Vector2(speed * h, rb2d.velocity.y);


        if (grounded && Input.GetKeyDown(KeyCode.W))
        {
            rb2d.AddForce(Vector2.up*jumpPower); 
        }

        if (grounded && Input.GetKey(KeyCode.S))
        {
            crouching = true;
            bc2d.offset=new Vector2(0.015f,-0.082f);
            bc2d.size = new Vector2(0.179f,0.153f);
            speed = 4f;
        }
        else
        {
            crouching = false;
            bc2d.offset=new Vector2(-0.004f,-0.054f);
            bc2d.size = new Vector2(0.160f,0.209f);
            speed = 7f;
        }

        




    }
    
    
}