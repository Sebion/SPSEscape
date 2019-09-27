
using System;
using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EagleAI : MonoBehaviour
{
    public Transform targer;
    //how many second we will update our path
    
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;
    
    //The calculated Path
    public Path path;
    
    //The AI speed
    public float speed = 300;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    
    // Max distance from AI to a waypoint for it to continue to next waypoint 
    public float nextWaypointDistance = 3;
    // Waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (targer == null)
        {
            Debug.Log("No player found!");
            return;
        }
        //Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, targer.position, OnPathComplete);

        StartCoroutine(UpdatePath());

        IEnumerator UpdatePath()
        {
            if (targer.Equals(null))
            {
                //TODO: Insert a player search here
                yield return null;
            }

            seeker.StartPath(transform.position, targer.position, OnPathComplete);

            yield return new WaitForSeconds( 1f/updateRate);
            StartCoroutine(UpdatePath());
        }

        void OnPathComplete(Path p)
        {
            Debug.Log("We got a path did it have an error ? " + p.error);
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }
        



    }

    private void FixedUpdate()
    {
        if (targer.Equals(null))
        {
            return;
          
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
               
            }
            Debug.Log("End of path reached");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;
        
        //Directions to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        
        //Move the AI
        rb.AddForce(dir,fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}


