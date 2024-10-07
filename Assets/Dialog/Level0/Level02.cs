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

    public GameObject 对话;
    public GameObject 对话框;
    public GameObject 名称;
    public GameObject[] 待隐藏;
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
            Destroy(对话.GetComponent<Level00>());
            对话.SetActive(true);
            Speak = 对话框.GetComponent<Text>();
            Name = 名称.GetComponent<Text>();
            Speaker = 对话.transform.Find("CatImage").GetComponent<Image>();
            foreach (GameObject obj in 待隐藏)
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
        DialogList.Add(new Dialog("香蕉猫", "喵喵救我！", Companion));
        DialogList.Add(new Dialog("喵喵", "别担心，我一定能救你出来（小声）", Player));
        DialogList.Add(new Dialog("香蕉猫", "我还有三包没过期的小鱼干一包大甩卖的猫粮" +
            "没吃我妈妈还在家里等我吃饭我从小一起长大的红嘴青臂鹦鹉还没有喂食你一定要救我啊啊啊啊！！！", Companion));
        DialogList.Add(new Dialog("喵喵", "知道了！！！！！（大声）", Player));


        
    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassed02", 1);
        FindObjectOfType<GameManager>().stop = false;
        foreach (GameObject obj in 待隐藏)
        {
            obj.SetActive(true);
        }
        GetComponent<Level02>().enabled=false;
        对话.SetActive(false);
    }
}
