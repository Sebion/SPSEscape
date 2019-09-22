using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Player player;
    private BoxCollider2D groundChecker;

    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        groundChecker = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag!= "Unjumpable")
        {
            
            player.grounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.tag != "Unjumpable")
        {
            player.grounded = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        player.grounded = false;
    }


}