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

    public GameObject 对话框;
    public GameObject 名称;
    public GameObject[] 待隐藏;
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
            Speak = 对话框.GetComponent<Text>();
            Name = 名称.GetComponent<Text>();
            Speaker = transform.Find("CatImage").GetComponent<Image>();
            origin = Speaker.color;
            foreach (GameObject obj in 待隐藏)
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
        DialogList.Add(new Dialog("知识猫、香蕉猫", "啊啊啊！！！！", Companion));
        DialogList.Add(new Dialog("喵喵", "发生什么事了", Player));
        DialogList.Add(new Dialog("", "两只喵星人被困在了笼子里！", null));
        DialogList.Add(new Dialog("知识猫、香蕉猫", "我们好像被这里的陷阱困住了", Companion));
        DialogList.Add(new Dialog("喵喵", "怎么办怎么办怎么办...（四处张望）", Player));
        DialogList.Add(new Dialog("", "喵喵忽然发现远方有几个奇异的魔法阵，形状似乎与笼子相吻合", null));
        DialogList.Add(new Dialog("喵喵", "莫非，把笼子推到那里，就能够救出他们了吗", Player));
        DialogList.Add(new Dialog("喵喵", "不管了，先试一下，不能坐视不管了！", Player));
        DialogList.Add(new Dialog("", "在喵喵准备动身的一刹那，头盔似乎受到了一些遗迹魔法的影响！", null));
        DialogList.Add(new Dialog("", "喵喵突然感受到了周围来自陷阱的恶意", null));
        DialogList.Add(new Dialog("喵喵", "头好痛，要长脑子了...", Player));
        DialogList.Add(new Dialog("喵喵", "如果我能借此避开这些陷阱的话...", Player));
    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassed00", 1);
        //FindObjectOfType<GameManager>().stop = false;
        foreach(GameObject obj in 待隐藏)
        {
            obj.SetActive(true);
        }
        Next.SetActive(true);
        FindObjectOfType<GameManager>().stop = false;
        gameObject.SetActive(false);
    }
}
