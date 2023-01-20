using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect_Gem : Collect
{
    // [SerializeField] private int Gem_Num;
    // [SerializeField] private Text GemText;
    [SerializeField] private AudioSource GemAudio;
    private SpriteRenderer SpriteRenderer;
    private CircleCollider2D CircleCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        GemAudio = GetComponent<AudioSource>();
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

            // Gem_Num += 1;
            
            GemAudio.Play();

            // GemText.text = Gem_Num.ToString();
            
            Destroy(gameObject , 0.4f);
        }
    }

    
}
