using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartIntroduction : MonoBehaviour
{
    private Text Speak;
    private Dialog current;
    private List<Dialog> DialogList;
    private float time;
    private bool stop;

    public GameObject 对话框;
    public float ShowTimePerWord = 3f;

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
    // Start is called before the first frame update
    void Start()
    {
        Speak = 对话框.GetComponent<Text>();
        FindObjectOfType<GameManager>().stop = true;
        current = null;
        Initialize();
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
                Speak.text = "";
                current.length = 1;
                time = ShowTimePerWord;
                stop = false;
            }
            else Finish();
        }
        time += Time.fixedDeltaTime;
        if (!stop && time >= ShowTimePerWord)
        {
            Speak.text = current.words.Substring(0, current.length);
            current.length++;
            if (current.length > current.words.Length) stop = true;
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stop) current = null;
            else current.length = current.words.Length;
        }
    }
    private void Initialize()
    {
        DialogList = new List<Dialog>();
        DialogList.Add(new Dialog("浩瀚的宇宙里，存在着一颗人类暂未发现的星体，我们不妨称它为喵星球，顾名思义，在那个星球上，猫咪们就如同人类般世世代代生活着。"));
        DialogList.Add(new Dialog("在这个星球的某个位置坐落着一个村子，村里住着一个胆子极小的猫猫，名叫喵喵，喜欢戴着一个不知从何处捡来的头盔。"));
        DialogList.Add(new Dialog("有一天，一只名叫喵喵的小猫与他的伙伴们一如既往地去远方探险，不过这一次，他们找到了一个从未发现的遗迹——"));
        DialogList.Add(new Dialog("他们七分兴奋，三分好奇地探索着遗迹，突然————————————"));
    }
    private void Finish()
    {
        FindObjectOfType<GameManager>().stop = false;
        gameObject.SetActive(false);
    }
}
