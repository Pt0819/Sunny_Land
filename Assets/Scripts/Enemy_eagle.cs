using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{

    // [SerializeField] private Rigidbody2D rb;
    //上下边界
    [SerializeField] private Transform upPos, downPos;
    [SerializeField] private float upBound;
    [SerializeField] private float downBound;

    [SerializeField] private float flyspeed;
    // [SerializeField] private Collider2D collider;
    [SerializeField] private bool isUp = true;
    
    //动画
    // [SerializeField] private Animator anime;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        // collider = GetComponent<Collider2D>();
        upBound = upPos.position.y;
        downBound = downPos.position.y;
        Destroy(upPos.gameObject);
        Destroy(downPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x,flyspeed);
            if (transform.position.y >= upBound)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,-flyspeed);
            if (transform.position.y <= downBound)
            {
                isUp = true;
            }
        }
        
    }
    

}
