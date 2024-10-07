using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level03 : MonoBehaviour
{
    private float time = 0;
    private int start = 0;//0:没点开始，1：黑色背景加载中，2：黑色背景加载完成，3：进入游戏
    public GameObject Background;
    public float ChangeTime=0.1f;//变黑时间


    private Text Speak;
    private Dialog current;
    private List<Dialog> DialogList;
    private float wordTime;
    private bool stop;//是否暂停打字
    private AudioSource audioSource;

    public GameObject 对话框;
    public float ShowTimePerWord = 0.5f;
    class Dialog
    {
        public string words;
        public int length;
        public Dialog(string words)
        {
            this.words = words;
            length = 1;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        Destroy(GameObject.Find("BGM"));
        start = 1;
        Background.SetActive(true);
        Speak = 对话框.GetComponent<Text>();
        current = null;
        Initialize();
    }
    void FixedUpdate()
    {
        //打字效果
        if (start == 2 && !FindObjectOfType<GameManager>().setting)
        {
            if (current == null)
            {
                if (DialogList.Count > 0)
                {
                    audioSource.Stop();
                    current = DialogList[0];
                    DialogList.RemoveAt(0);
                    Speak.text = "";
                    current.length = 1;
                    wordTime = ShowTimePerWord;
                    stop = false;
                    audioSource.Play();
                }
                else start = 3;
            }
            wordTime += Time.fixedDeltaTime;
            if (!stop && wordTime >= ShowTimePerWord)
            {
                Speak.text = current.words.Substring(0, current.length);
                current.length++;
                wordTime = 0;
                if (current.length > current.words.Length) stop = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(stop) audioSource.Stop();
        if (start == 3)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (start == 2 && Input.GetMouseButtonDown(0))
        {
            if (stop) current = null;
            else current.length = current.words.Length;
        }
        if (start == 1 && time<ChangeTime)
        {
            time += Time.deltaTime;
            Color origin = Background.GetComponent<Image>().color;
            Background.GetComponent<Image>().color = new Color(origin.r, origin.g, origin.b, origin.a + Time.deltaTime/ChangeTime);
        }
        if(Background.GetComponent<Image>().color.a >= 1)
        {
            start = 2;//可以加载文字
        }
    }
    private void Initialize()
    {
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("笼子应声而开，喵喵成功救出了他的一名伙伴。"));
        DialogList.Add(new Dialog("没来得及高兴，喵喵将目光远眺\n另一名同伴就在不远处。不过————"));
        DialogList.Add(new Dialog("那里的恶意更加浓厚，喵喵不禁有些退缩\n犹豫再三，喵喵毅然向香蕉猫迈出了勇敢的一步..."));
    }
}
