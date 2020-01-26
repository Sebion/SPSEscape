using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
   

    private float movementSpeed;

    private float upDownInterval;
    private float speedOfInterval;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        movementSpeed = Random.Range(-2f, -5f);
        upDownInterval = Random.Range(1f, 4f);
        speedOfInterval = Random.Range(1f,4f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        rb2d.velocity=new Vector2(movementSpeed,Mathf.Sin(Time.time*speedOfInterval)*upDownInterval);
    }
}
