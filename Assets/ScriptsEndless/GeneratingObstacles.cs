using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GeneratingObstacles : MonoBehaviour
{
    public Transform generationPoint;
    public ObjectPooler[] objectPools;

    private float gapWidth;
    public float gapWidthMax;
    public float gapWidthMin;

    public int positionZ;

    private float obstacleWidth;
    private int obstacleSelector;
    private float[] obstacleWidths;

    // Start is called before the first frame update
    void Start()
    {
        obstacleWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++)
        {
            obstacleWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.transform.position.x)
        {
            gapWidth = Random.Range(gapWidthMin, gapWidthMax);
            obstacleSelector = Random.Range(0, objectPools.Length);
            transform.position = new Vector3(transform.position.x + gapWidth + (obstacleWidths[obstacleSelector]/2),
                transform.position.y,
                positionZ);


            GameObject newObstacle = objectPools[obstacleSelector].getPooledObject();
            newObstacle.transform.position = transform.position;
            newObstacle.transform.rotation = transform.rotation;
            newObstacle.SetActive(true);
            
            transform.position = new Vector3(transform.position.x + gapWidth + (obstacleWidths[obstacleSelector]/2),
                transform.position.y,
                positionZ);
        }
    }
}