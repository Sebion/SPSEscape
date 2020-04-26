using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;


public class PillPicker : MonoBehaviour
{
    public AudioSource pickupSound;
    public int bonusScore;
    public GameManager GameManager;
    private Rigidbody2D rb2d;
    public PostProcessVolume _postProcessVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb2d.velocity=new Vector2(rb2d.velocity.x,Mathf.Sin(Time.time*5)*2);
        
    }
   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            pickupSound.Play();
            GameManager.AddScore(bonusScore);
            StartCoroutine(pillEffect());
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            
           
        }
    }

    private IEnumerator pillEffect()
    {
        
        float[] randomTimeScale={0.3f,0.5f,0.7f,1.2f,1.5f,2f};
        int random;
        float randomWeight;
        for (int i = 0; i < 5; i++)
        {
            randomWeight = Random.Range(0.5f, 1f);
            _postProcessVolume.weight = randomWeight;
            random = Random.Range(0, 5);
            Time.timeScale = randomTimeScale[random];
            Debug.Log(Time.timeScale+"pocas efektu");
            yield return new WaitForSeconds(0.7f);
        }
        
        Time.timeScale = 1f;
        _postProcessVolume.weight = 0;
        Debug.Log("skoncil efekt a time scale je "+Time.timeScale);
        gameObject.SetActive(false);
        
    }
}