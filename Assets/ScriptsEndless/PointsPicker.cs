
using System;
using UnityEngine;


public class PointsPicker : MonoBehaviour
{
    public AudioSource pickupSound;
    public int bonusScore;
    public GameManager GameManager;
    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb2d.velocity=new Vector2(rb2d.velocity.x,Mathf.Sin(Time.time*5)*2);
    }
   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            pickupSound.Play();
            GameManager.AddScore(bonusScore);
            gameObject.SetActive(false);
        }
    }
}
