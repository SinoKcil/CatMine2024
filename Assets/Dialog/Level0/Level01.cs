using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level01 : MonoBehaviour
{
    private Text Speak;
    private Dialog current;
    private List<Dialog> DialogList;

    public GameObject �Ի���;
    public GameObject[] ������;
    public Sprite Companion;
    public Sprite Player;

    class Dialog 
    {
        public string words;
        public string operation;
        public int length;
        public Dialog(string words,string op)
        {
            this.words = words;
            this.operation = op;
            length = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HavePassedTeach")) Finish();
        Speak=�Ի���.GetComponent<Text>();
        foreach(GameObject obj in ������)
        {
            obj.SetActive(false);
        }
        current = null;
        Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        if (current == null)
        {
            if (DialogList.Count > 0)
            {
                current = DialogList[0];
                DialogList.RemoveAt(0);
                Speak.text = current.words;
                current.length = 1;
            }
            else Finish();
        }
        if ((string.IsNullOrEmpty(current.operation)&&Input.anyKeyDown)||Input.GetKeyDown(current.operation))
        {
            current = null;
        }
    }
    private void Initialize()
    {
        DialogList=new List<Dialog>();
        DialogList.Add(new Dialog("������������ô���ǲݰ�ι", null));
        DialogList.Add(new Dialog("����֪��������Ʈ����һ��������", null));
        DialogList.Add(new Dialog("��ʥ��������",null));

    }
    private void Finish()
    {
        PlayerPrefs.SetInt("HavePassedTeach", 1);
        foreach(GameObject obj in ������)
        {
            obj.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
