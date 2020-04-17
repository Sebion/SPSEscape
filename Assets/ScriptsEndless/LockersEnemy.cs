using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockersEnemy : MonoBehaviour
{
    public GameObject playerEndless;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        playerEndless=GameObject.Find("PlayerEndless");
        anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerEndless.transform.position) < 3)
        {
            anim.SetTrigger("Enemy");
        }
    }

}


