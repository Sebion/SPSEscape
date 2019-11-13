using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float smoothTime = 0.2f;
    private float velocity;
   
   
    // Update is called once per frame
    void Update()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x , ref velocity, smoothTime);
        transform.position = new Vector3(posX,0,transform.position.z);   
    }
}
