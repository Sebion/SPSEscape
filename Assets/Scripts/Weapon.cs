using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bullet;
    public Player player;

    // Update is called once per frame
    private void Start()
    {
        
    }

    void Update()
    {
        if (!player.crouching && Input.GetKeyDown(KeyCode.E))
        {
            shoot();
        }
        
    }
 
    private void shoot()
    {
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
    }
}
