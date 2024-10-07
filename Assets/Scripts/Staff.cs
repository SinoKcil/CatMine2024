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

    public float 未完待续=1.0f;
    public float 滚动=5.0f;
    public float 暂停 = 3.0f;
    public GameObject 感谢菜单;
    public GameObject CG;
    // Start is called before the first frame update
    void Start()
    {
        string nothiddenflag = "Lilac{m30w_meow_m3ow_me0w}";
        Debug.Log(nothiddenflag);
        end = transform.Find("未完待续").gameObject;
        OriginPlace = transform.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        if (endtime < 未完待续)
        {
            endtime += Time.deltaTime;
            Text text=end.GetComponent<Text>();
            text.color=new Color(text.color.r,text.color.g,text.color.b,
                1-endtime/未完待续);
        }
        else if (transform.GetComponent<RectTransform>().position.y<600)
        {
            CG.SetActive(true);
            scrolltime += Time.deltaTime;
            transform.GetComponent<RectTransform>().position
                = new Vector3(OriginPlace.x, OriginPlace.y - OriginPlace.y * scrolltime / 滚动, OriginPlace.z);
        }
        else if(stoptime < 暂停)
        {
            stoptime += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            感谢菜单.SetActive(true);
        }
    }
}
