using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed = 7f;
    public float jumpPower = 150f;
    public bool grounded=true;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed",Math.Abs(Input.GetAxis("Horizontal")));
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
       // rb2d.AddForce((Vector2.right * speed) * h);
       rb2d.velocity = new Vector2(speed*h, rb2d.velocity.y);
       
    }
}