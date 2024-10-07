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
    private int start = 0;//0:û�㿪ʼ��1����ɫ���������У�2����ɫ����������ɣ�3��������Ϸ
    public GameObject Background;
    public float ChangeTime=0.1f;//���ʱ��


    private Text Speak;
    private Dialog current;
    private List<Dialog> DialogList;
    private float wordTime;
    private bool stop;//�Ƿ�ֹͣ����
    private AudioSource audioSource;

    public GameObject �Ի���;
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
        Speak = �Ի���.GetComponent<Text>();
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
        if (start >= 1 && Input.anyKeyDown)//ֹͣ����
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
            start = 2;//���Լ�������
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
                start = 1;//��ʼ���䱳��
            }
        }
    }
    private void Initialize()
    {
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("��嫵�������\n������һ��������δ���ֵ�����\n���ǲ�������Ϊ������\n����˼�壬���Ǹ�������\nè���Ǿ���ͬ�������������������"));
        DialogList.Add(new Dialog("����������ĳ��λ��������һ������\n����ס��һ�����Ӽ�С��èè����������\nϲ������һ����֪�Ӻδ�������ͷ��"));
        DialogList.Add(new Dialog("��һ��\nһֻ����������Сè�����Ļ����һ�������ȥԶ��̽��\n������һ�Σ������ҵ���һ����δ���ֵ��ż�����"));
        DialogList.Add(new Dialog("�����߷��˷ܣ����ֺ����̽�����ż�\nͻȻ������������������"));
    }
}
