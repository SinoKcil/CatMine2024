using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Level00 : MonoBehaviour
{
    private Text Speak;
    private Text Name;
    private Image Speaker;
    private Dialog current;
    private List<Dialog> DialogList;
    private float time;
    private bool stop;
    UnityEngine.Color origin;

    public GameObject �Ի���;
    public GameObject ����;
    public GameObject[] ������;
    public Sprite Companion;
    public Sprite Player;
    public float ShowTimePerWord=0.05f;
    public GameObject Next;

    class Dialog 
    {
        public string character;
        public string words;
        public Sprite image;
        public int length;
        public Dialog(string character, string words, Sprite image)
        {
            this.character = character;
            this.words = words;
            this.image = image;
            length = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("HavePassed00"))
        {
            Speak = �Ի���.GetComponent<Text>();
            Name = ����.GetComponent<Text>();
            Speaker = transform.Find("CatImage").GetComponent<Image>();
            origin = Speaker.color;
            foreach (GameObject obj in ������)
            {
                obj.SetActive(false);
            }
            FindObjectOfType<GameManager>().stop = true;
            current = null;
            Initialize();
        }
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current == null) 
        {
            if (DialogList.Count > 0)
            {
                current = DialogList[0];
                DialogList.RemoveAt(0);
                Name.text = current.character;
                Speak.text = "";
                Speaker.sprite = current.image;
                Speaker.color = origin;
                if (Speaker.sprite == null)
                {
                    UnityEngine.Color color = new UnityEngine.Color(origin.r, origin.g, origin.b, 0);
                    Speaker.color = color;
                }
                current.length = 1;
                time = ShowTimePerWord;
                stop = false;
            }
            else
            {
                Finish();
            }
        }
        time += Time.fixedDeltaTime;
        if (!stop&&time>=ShowTimePerWord && !FindObjectOfType<GameManager>().setting)
        {
            Speak.text = current.words.Substring(0, current.length);
            current.length++;
            time = 0.0f;
            if (current.length > current.words.Length) stop = true;
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !FindObjectOfType<GameManager>().setting)
        {
            if (stop) current = null;
            else current.length = current.words.Length;
        }
    }
    private void Initialize()
    {
        DialogList=new List<Dialog>();
        DialogList.Add(new Dialog("֪ʶè���㽶è", "��������������", Companion));
        DialogList.Add(new Dialog("����", "����ʲô����", Player));
        DialogList.Add(new Dialog("", "��ֻ�����˱������������", null));
        DialogList.Add(new Dialog("֪ʶè���㽶è", "���Ǻ��������������ס��", Companion));
        DialogList.Add(new Dialog("����", "��ô����ô����ô��...���Ĵ�������", Player));
        DialogList.Add(new Dialog("", "������Ȼ����Զ���м��������ħ������״�ƺ����������Ǻ�", null));
        DialogList.Add(new Dialog("����", "Ī�ǣ��������Ƶ�������ܹ��ȳ���������", Player));
        DialogList.Add(new Dialog("����", "�����ˣ�����һ�£��������Ӳ����ˣ�", Player));
        DialogList.Add(new Dialog("", "������׼�������һɲ�ǣ�ͷ���ƺ��ܵ���һЩ�ż�ħ����Ӱ�죡", null));
        DialogList.Add(new Dialog("", "����ͻȻ���ܵ�����Χ��������Ķ���", null));
        DialogList.Add(new Dialog("����", "ͷ��ʹ��Ҫ��������...", Player));
        DialogList.Add(new Dialog("����", "������ܽ�˱ܿ���Щ����Ļ�...", Player));
    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassed00", 1);
        //FindObjectOfType<GameManager>().stop = false;
        foreach(GameObject obj in ������)
        {
            obj.SetActive(true);
        }
        Next.SetActive(true);
        FindObjectOfType<GameManager>().stop = false;
        gameObject.SetActive(false);
    }
}
