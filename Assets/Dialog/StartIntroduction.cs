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

    public GameObject �Ի���;
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
        Speak = �Ի���.GetComponent<Text>();
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
        DialogList.Add(new Dialog("��嫵������������һ��������δ���ֵ����壬���ǲ�������Ϊ�����򣬹���˼�壬���Ǹ������ϣ�è���Ǿ���ͬ������������������š�"));
        DialogList.Add(new Dialog("����������ĳ��λ��������һ�����ӣ�����ס��һ�����Ӽ�С��èè������������ϲ������һ����֪�Ӻδ�������ͷ����"));
        DialogList.Add(new Dialog("��һ�죬һֻ����������Сè�����Ļ����һ�������ȥԶ��̽�գ�������һ�Σ������ҵ���һ����δ���ֵ��ż�����"));
        DialogList.Add(new Dialog("�����߷��˷ܣ����ֺ����̽�����ż���ͻȻ������������������������"));
    }
    private void Finish()
    {
        FindObjectOfType<GameManager>().stop = false;
        gameObject.SetActive(false);
    }
}
