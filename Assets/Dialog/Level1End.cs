using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1End : MonoBehaviour
{
    private Text Speak;
    private Text Name;
    private Image Speaker;
    private Dialog current;
    private List<Dialog> DialogList;
    private float time;
    private bool stop;
    //private AudioSource audioSource;
    UnityEngine.Color origin;

    public GameObject �Ի�;
    public GameObject �Ի���;
    public GameObject ����;
    public GameObject[] ������;
    public Sprite Companion;
    public Sprite Player;
    public float ShowTimePerWord = 0.05f;

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
    private void Start()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        //audioSource = GetComponent<AudioSource>();
        current = null;
        Initialize();
    }
    void OnEnable()
    {
        Destroy(GameObject.Find("ͬ��A"));
        //if (PlayerPrefs.HasKey("HavePassed0")) Finish();
        �Ի�.SetActive(true);
        Speak = �Ի���.GetComponent<Text>();
        Name = ����.GetComponent<Text>();
        Speaker = �Ի�.transform.Find("CatImage").GetComponent<Image>();
        origin = Speaker.color;
        foreach (GameObject obj in ������)
        {
            obj.SetActive(false);
        }
        FindObjectOfType<GameManager>().stop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current == null)
        {
            if (DialogList.Count > 0)
            {
                //audioSource.Stop();
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
                else Speak.color = new UnityEngine.Color(origin.r, origin.g, origin.b, 255);
                current.length = 1;
                time = ShowTimePerWord;
                stop = false;
                //audioSource.Play();
            }
            else Finish();
        }
        time += Time.fixedDeltaTime;
        if (!stop && time >= ShowTimePerWord)
        {
            Speak.text = current.words.Substring(0, current.length);
            current.length++;
            time = 0.0f;
            if (current.length > current.words.Length) stop = true;
        }

    }
    private void Update()
    {
        //if(stop) audioSource.Stop();
        if (Input.GetMouseButtonDown(0))
        {
            if (stop) current = null;
            else current.length = current.words.Length;
        }
    }
    private void Initialize()
    {
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("����", "����û�°�", Player));
        DialogList.Add(new Dialog("֪ʶè���㽶è", "������㣡��ʱ���뿪����Ƿ�֮����", Companion));
        DialogList.Add(new Dialog("", "����û�лش�ֻ�ǿ����ż����", null));
        DialogList.Add(new Dialog("", "����ɢ�����Ķ���ǰ��δ�е��أ��������һ��ħ����������ͷ��", null));
        DialogList.Add(new Dialog("����", "�������ߣ���Ҫһ̽������", Player));
        DialogList.Add(new Dialog("", "�������������ż����û�л�ͷ......", null));
    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassed0", 1);
        FindObjectOfType<GameManager>().stop = false;
        �Ի�.SetActive(false);
        GameObject.Find("UI").SetActive(false);
        transform.Find("BlackMask").gameObject.SetActive(true);
    }
}
