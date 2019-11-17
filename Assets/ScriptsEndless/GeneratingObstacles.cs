using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public Transform generationPoint;
    public ObjectPooler objectPooler;
    private float gapWidth;
    public float gapWidthMax;
    public float gapWidthMin;
    public int positionZ;
    private float obstacleWidth;

    // Start is called before the first frame update
    void Start()
    {
        if (!obstacle.GetComponent<BoxCollider2D>().Equals(null))
        {
            obstacleWidth = obstacle.GetComponent<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.transform.position.x)
        {
            gapWidth = Random.Range(gapWidthMin, gapWidthMax);
            transform.position = new Vector3(transform.position.x + gapWidth + obstacleWidth, transform.position.y,
                positionZ);
            // Instantiate(obstacle, transform.position, transform.rotation);
            GameObject newObstacle = objectPooler.getPooledObject();
            newObstacle.transform.position = transform.position;
            newObstacle.transform.rotation = transform.rotation;
            newObstacle.SetActive(true);
        }
    }
}