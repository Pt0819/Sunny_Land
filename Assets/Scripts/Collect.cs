using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    //收集品Effect
    public GameObject collectEffect;
    
    private SpriteRenderer SpriteRenderer;
    private CircleCollider2D CircleCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
    }

    // protected virtual void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Player")
    //     {
    //         SpriteRenderer.enabled = false;
    //         CircleCollider2D.enabled = false;
    //         
    //         collectEffect.SetActive(true);
    //         
    //         Destroy(gameObject , 0.3f);
    //     }
    // }
    
}
