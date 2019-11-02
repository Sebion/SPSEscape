using UnityEngine;

namespace ScriptsEndless
{
    public class CameraEndless : MonoBehaviour
    {
        public Transform player;
        public float smoothTime = 0.2f;
        private float velocity;
   
   
        // Update is called once per frame
        void FixedUpdate()
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x , ref velocity, smoothTime);
            transform.position = new Vector3(posX,0,transform.position.z);   
        }
    }
}