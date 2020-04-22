using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CoinGenerator : MonoBehaviour
{
    public Transform generationPoint;
    public ObjectPooler[] objectPools;
    private int obstacleSelector;
    


    public float distanceBetweenCoins;
    private float coinY;

    private void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x < generationPoint.transform.position.x)
        {
            coinY = Random.Range(0, 3);
            obstacleSelector = Random.Range(0, 7);
            distanceBetweenCoins = Random.Range(10, 15);
            

            if (obstacleSelector <=5)
            {
                SpawnCoin(0);
            }

            if (obstacleSelector >= 6)
            {
                SpawnCoin(1);
            }
        }
    }
    
   

    private void SpawnCoin(int indexOfCoin)
    {
        transform.position =
            new Vector3(transform.position.x + distanceBetweenCoins, coinY, transform.position.z);

        GameObject newObstacle = objectPools[indexOfCoin].getPooledObject();
        newObstacle.transform.position = transform.position;
        newObstacle.transform.rotation = transform.rotation;
        newObstacle.SetActive(true);
        

        transform.position =
            new Vector3(transform.position.x + distanceBetweenCoins, coinY, transform.position.z);
    }
}