using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //获得角色的刚体控制移动
    [SerializeField]private Rigidbody2D rb;
    //控制角色的动画
    [SerializeField]private Animator anime;
    
    //角色的移动速度
    public float speed;
    
    //角色的跳跃力
    public float JumpY;
    
    //获取角色collider
    public Collider2D collider;
    [SerializeField] private Collider2D disCollider;

    //地面检测,辅助二段跳
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGround;
    
    //头顶检测，用于爬行时检测头顶是否包含障碍物
    [SerializeField] private Transform cellingCheck;
    
    //检测跳跃bool，跳跃次数
    [SerializeField] private bool isJump;
    [SerializeField] private int jumpCount;
    [SerializeField] private bool jumpPressed;
    
    //收集品计数器_Cherry,gem
    public int Cherry = 0;
    public int Gem = 0;
    
    //修改Cherry,gem的Text显示
    public Text CherryNum_Text;
    public Text GemNum_Text;
    
    //受伤检测
    [SerializeField] private bool isHurt = false;
    
    //角色声音变量
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource HurtAudio;
    // [SerializeField] private AudioSource CherryAudio;
    // [SerializeField] private AudioSource GemAudio;



    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Game Start");
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }
    
    void FixedUpdate()
    {
        // SlopeCheck();
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);

        if (!isHurt)
        {
            Movement();
        }

        Jump();
        
        SwitchAnime();
        
    }
    
    //斜坡检测函数
    private void SlopeCheck()
    {
        
    }

    //角色的移动
    void Movement()
    {
        //GetAxisRaw方法返回固定值-1,0,1,水平变量，控制角色面朝方向
        float herizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(herizontalMove * speed , rb.velocity.y);
        
        
        
        //转换水平方向朝向，对应transform中的scale(z轴)影响人物大小，因此采用Vector3
        if (herizontalMove != 0)
        {
            transform.localScale = new Vector3(herizontalMove , 1 , 1);
        }

        Crouch();
    }
    
    private void Jump()
    {

        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        
        //判断是否在游戏中按下按键Jump且角色在地面上
        if (jumpPressed && isGround)
        {
            isJump = true;
            JumpAudio.Play();
            rb.velocity = new Vector2(0, JumpY);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0) //此时在空中，进行二段跳判断
        {
            JumpAudio.Play();
            rb.velocity = new Vector2(0 , JumpY);
            jumpCount--;
            jumpPressed = false;
        }

    }
    
    //角色跳跃切换动画
    private void SwitchAnime()
    {
        anime.SetBool("idle" , false);
        //
        // if (anime.GetBool("jumping"))
        // {
        //
        //     if (rb.velocity.y < 0)
        //     {
        //         anime.SetBool("jumping" , false);
        //         anime.SetBool("falling" , true);
        //         
        //     }
        // }
        // else if (collider.IsTouchingLayers(ground))
        // {
        //     anime.SetBool("falling" , false);
        //     anime.SetBool("idle" , true);
        // }
        
        anime.SetFloat("running" , Math.Abs(rb.velocity.x));

        if (rb.velocity.y < 0.1f && !isGround)
        {
            anime.SetBool("falling" , true);
        }
        //受伤切换
        if (isHurt)
        {
            anime.SetBool("hurt" , true);
            anime.SetFloat("running" , 0f);
            if (Math.Abs(rb.velocity.x) < 0.1f)
            {
                anime.SetBool("hurt" , false);
                anime.SetBool("idle" , true);
                isHurt = false;
            }
        }

        if (isGround)
        {
            anime.SetBool("falling" , false);
            anime.SetBool("idle" , true);
            
        }
        else if (!isGround && rb.velocity.y>0) //y轴速度大于0=>角色上升
        {
            anime.SetBool("jumping" , true);
        }
        else if (rb.velocity.y < 0)
        {
            anime.SetBool("jumping" , false);
            anime.SetBool("falling" , true);
        }
    }
    
    //角色Trigger碰撞器
    private void OnTriggerEnter2D(Collider2D col)
    {
        //如果碰撞器碰撞属于Collections的标签，消除次Object
        //角色收集
        if (col.tag == "Cherry")
        {
            // CherryAudio.Play();
            // Destroy(col.gameObject);
            Cherry += 1;
            CherryNum_Text.text = Cherry.ToString();
        }
        
        if (col.tag == "Gem")
        {
            // GemAudio.Play();
            // Destroy(col.gameObject);
            Gem += 1;
            GemNum_Text.text = Gem.ToString();
        }
        
        //如果掉出地图,延迟触发重新加载
        if (col.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("ReStart" , 2f);
        }

    }
    
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Enemies")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            
            if (anime.GetBool("falling")&& transform.position.y > (col.gameObject.transform.position.y+1))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x * Time.fixedDeltaTime, JumpY);
                anime.SetBool("jumping" , true);
            }
            else if (transform.position.x < col.gameObject.transform.position.x) //判断角色在敌人的左侧
            {
                rb.velocity = new Vector2(-4.5f, rb.velocity.y);
                isHurt = true;
                HurtAudio.Play();
            }
            else if (transform.position.x > col.gameObject.transform.position.x) //判断角色在敌人的右侧
            {
                rb.velocity = new Vector2(4.5f, rb.velocity.y);
                isHurt = true;
                HurtAudio.Play();
            }
        }
    }
    
    //下蹲
    private void Crouch()
    {
        //在点cellingCheck的位置附近半径0.2f检查是否包含ground
        if (!Physics2D.OverlapCircle(cellingCheck.position , 0.2f , ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anime.SetBool("crouching" , true);
                //取消头部碰撞器
                disCollider.enabled = false;
            }
            else
            {
                anime.SetBool("crouching" , false);
                disCollider.enabled = true;
            }
        }
    }
    
    //游戏重新加载
    private void ReStart()
    {
        //出界坠落
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
