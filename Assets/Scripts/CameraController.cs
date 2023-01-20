using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //通过更改Main Camera的Transform项达到摄像机跟随角色移动
    //角色的transform
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //使camera的transform和角色的移动相关
        transform.position = new Vector3(player.position.x ,player.position.y , -10f);
    }
}
