
using UnityEngine;


public class PointsPicker : MonoBehaviour
{

    public int bonusScore;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            
            GameManager.AddScore(bonusScore);
            gameObject.SetActive(false);
        }
    }
}
