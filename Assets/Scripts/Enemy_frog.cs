using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Enemy_frog : Enemy
{

    // [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftPos, rightPos;
    [SerializeField] private float speed , jumpForce;

    [SerializeField] private bool Faceleft = true;

    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    
    //青蛙跳跃
    [SerializeField] private Collider2D collider;
    [SerializeField] private LayerMask ground;
    //动画
    // [SerializeField] private Animator anime;
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        collider = GetComponent<Collider2D>();
        leftBound = leftPos.position.x;
        rightBound = rightPos.position.x;
        Destroy(leftPos.gameObject);
        Destroy(rightPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switchAnime();
    }
    

    private void Movement()
    {
        if (Faceleft)
        {
            if (collider.IsTouchingLayers(ground))
            {
                anime.SetBool("jumping",true);
                rb.velocity = new Vector2(-speed , jumpForce);
            }
            if (transform.position.x <= leftBound)
            {
                Faceleft = false;
                transform.localScale = new Vector3(-1,1,1);
                
            }
        }
        else
        {
            if (collider.IsTouchingLayers(ground))
            {
                anime.SetBool("jumping",true);
                rb.velocity = new Vector2(speed , jumpForce);
            }
            if (transform.position.x >= rightBound)
            {
                Faceleft = true;
                transform.localScale = new Vector3(1,1,1);
            }
        }
    }

    private void switchAnime()
    {
        if (anime.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anime.SetBool("falling",true);
                anime.SetBool("jumping",false);
            }
        }

        if (collider.IsTouchingLayers(ground) && anime.GetBool("falling"))
        {
            anime.SetBool("falling",false);
        }
    }
    
}
