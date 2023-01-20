using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect_Cherry : Collect
{
    // [SerializeField] private int Cherry_Num;
    // [SerializeField] private Text CherryText;
    [SerializeField] private AudioSource CherryAudio;
    private SpriteRenderer SpriteRenderer;
    private CircleCollider2D CircleCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        CherryAudio = GetComponent<AudioSource>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SpriteRenderer.enabled = false;
            CircleCollider2D.enabled = false;
            
            collectEffect.SetActive(true);
            
            
            CherryAudio.Play();

            // CherryText.text = Cherry_Num.ToString();
            
            Destroy(gameObject , 0.4f);
        }
    }
}
