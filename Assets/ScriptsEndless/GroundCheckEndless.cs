using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckEndless : MonoBehaviour
{
    private PlayerEndless player;
    public AudioSource landingSound;

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerEndless>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Unjumpable")&& player.falling.Equals(true))
        {
            player.grounded = true;
            landingSound.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Unjumpable"))
        {
            player.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        player.grounded = false;
    }
}