using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staff : MonoBehaviour
{
    private float endtime = 0;
    private float scrolltime = 0;
    private float stoptime = 0;
    private GameObject end;
    private Vector3 OriginPlace;

    public float δ�����=1.0f;
    public float ����=5.0f;
    public float ��ͣ = 3.0f;
    public GameObject ��л�˵�;
    public GameObject CG;
    // Start is called before the first frame update
    void Start()
    {
        string nothiddenflag = "Lilac{m30w_meow_m3ow_me0w}";
        Debug.Log(nothiddenflag);
        end = transform.Find("δ�����").gameObject;
        OriginPlace = transform.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        if (endtime < δ�����)
        {
            endtime += Time.deltaTime;
            Text text=end.GetComponent<Text>();
            text.color=new Color(text.color.r,text.color.g,text.color.b,
                1-endtime/δ�����);
        }
        else if (transform.GetComponent<RectTransform>().position.y<600)
        {
            CG.SetActive(true);
            scrolltime += Time.deltaTime;
            transform.GetComponent<RectTransform>().position
                = new Vector3(OriginPlace.x, OriginPlace.y - OriginPlace.y * scrolltime / ����, OriginPlace.z);
        }
        else if(stoptime < ��ͣ)
        {
            stoptime += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            ��л�˵�.SetActive(true);
        }
    }
}
