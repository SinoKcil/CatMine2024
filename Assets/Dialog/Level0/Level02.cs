using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level02 : MonoBehaviour
{
    private Text Speak;
    private Text Name;
    private Image Speaker;
    private Dialog current;
    private List<Dialog> DialogList;
    private float time;
    private bool stop;
    private bool awake = false;

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
    void OnEnable()
    {
        if (PlayerPrefs.HasKey("HavePassed02")) Finish();
        else
        {
            Destroy(�Ի�.GetComponent<Level00>());
            �Ի�.SetActive(true);
            Speak = �Ի���.GetComponent<Text>();
            Name = ����.GetComponent<Text>();
            Speaker = �Ի�.transform.Find("CatImage").GetComponent<Image>();
            foreach (GameObject obj in ������)
            {
                obj.SetActive(false);
            }
            FindObjectOfType<GameManager>().stop = true;
            current = null;
            Initialize();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (awake)
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
                    current.length = 1;
                    time = ShowTimePerWord;
                    stop = false;
                }
                else Finish();
            }
            time += Time.fixedDeltaTime;
            if (!stop && time >= ShowTimePerWord&& !FindObjectOfType<GameManager>().setting)
            {
                Speak.text = current.words.Substring(0, current.length);
                current.length++;
                time = 0.0f;
                if (current.length > current.words.Length) stop = true;
            }
        }
    }
    private void Update()
    {
        if(awake && !FindObjectOfType<GameManager>().setting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (stop) current = null;
                else current.length = current.words.Length;
            }
        }
    }
    private void Initialize()
    {
        awake= true;
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("�㽶è", "�������ң�", Companion));
        DialogList.Add(new Dialog("����", "���ģ���һ���ܾ��������С����", Player));
        DialogList.Add(new Dialog("�㽶è", "�һ�������û���ڵ�С���һ����˦����è��" +
            "û�������軹�ڼ�����ҳԷ��Ҵ�Сһ�𳤴�ĺ���������Ļ�û��ιʳ��һ��Ҫ���Ұ�������������", Companion));
        DialogList.Add(new Dialog("����", "֪���ˣ�����������������", Player));


        
    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassed02", 1);
        FindObjectOfType<GameManager>().stop = false;
        foreach (GameObject obj in ������)
        {
            obj.SetActive(true);
        }
        GetComponent<Level02>().enabled=false;
        �Ի�.SetActive(false);
    }
}
