using System;
using UnityEngine;

namespace ScriptsEndless
{
    public class CameraEndless : MonoBehaviour
    {
        public PlayerEndless player;

        private float distanceToMove;
        private Vector3 lastPlayerPosition;


        private void Start()
        {
            lastPlayerPosition = player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            distanceToMove = player.transform.position.x - lastPlayerPosition.x;

            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y,
                transform.position.z);


            lastPlayerPosition = player.transform.position;
        }
    }
}