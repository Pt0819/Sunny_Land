using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("对话按钮")] public GameObject button;
    
    [Header("组件")] public Text TextLabel;

    [Header("文本文件")] public TextAsset TextFile;
    

    //对话编号
    public int index;
    
    public GameObject Dialogue;

    private List<string> textList = new List<string>();

    [SerializeField] private bool isUI;

    private void Awake()
    {
        GetTextFromFile(TextFile);
    }

    private void OnEnable()
    {
        TextLabel.text = textList[index++];
    }

    // Start is called before the first frame update
    void Start()
    {
        GetTextFromFile(TextFile);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        talkUI();
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            isUI = false;
            Dialogue.SetActive(false);
            index = 0;
        }
        if (isUI && Input.GetKeyDown(KeyCode.R))
        {
            TextLabel.text = textList[index];
            index++;
            
        }
    }

    private void GetTextFromFile(TextAsset File)
    {
        //重置List
        textList.Clear();
        index = 0;

        var LineData = File.text.Split('\n');
        //遍历文本文件
        foreach (var line in LineData)
        {
            textList.Add(line);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            button.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            button.SetActive(false);
            Dialogue.SetActive(false);
        }
    }

    private void talkUI()
    {
        if (button.activeSelf && Input.GetKey(KeyCode.R))
        {
            Dialogue.SetActive(true);
            isUI = true;
        }
    }
}
