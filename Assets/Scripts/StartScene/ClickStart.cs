using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickStart : MonoBehaviour,IPointerClickHandler
{
    private float time = 0;
    private int start = 0;//0:没点开始，1：黑色背景加载中，2：黑色背景加载完成，3：进入游戏
    public GameObject Background;
    public float ChangeTime=0.1f;//变黑时间


    private Text Speak;
    private Dialog current;
    private List<Dialog> DialogList;
    private float wordTime;
    private bool stop;//是否停止打字
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
    void Start()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        start = 0;
        Speak = 对话框.GetComponent<Text>();
        current = null;
        audioSource=GetComponent<AudioSource>();
        Initialize();
    }
    void FixedUpdate()
    {
        if (start == 2)
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
            SceneManager.LoadScene(1);
        }
        if (start == 2 && Input.GetMouseButtonDown(0))
        {
            if (stop) current = null;
            else current.length = current.words.Length;
        }
        if (start >= 1 && Input.anyKeyDown)//停止加载
        {
            start = 3;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(GameObject.Find("BGM"));
        if(eventData.button==PointerEventData.InputButton.Left)
        {
            if (Background != null)
            {
                Background.SetActive(true);
                start = 1;//开始渐变背景
            }
        }
    }
    private void Initialize()
    {
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("浩瀚的宇宙里\n存在着一颗人类暂未发现的星体\n我们不妨称它为喵星球\n顾名思义，在那个星球上\n猫咪们就如同人类般世世代代生活着"));
        DialogList.Add(new Dialog("在这个星球的某个位置坐落着一个村子\n村里住着一个胆子极小的猫猫，名叫喵喵\n喜欢戴着一个不知从何处捡来的头盔"));
        DialogList.Add(new Dialog("有一天\n一只名叫喵喵的小猫与他的伙伴们一如既往地去远方探险\n不过这一次，他们找到了一个从未发现的遗迹——"));
        DialogList.Add(new Dialog("他们七分兴奋，三分好奇地探索着遗迹\n突然—————————"));
    }
}
