using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    static float THICKNESS = 2;

    public LayerMask RoadMask;
    public LayerMask MineMask;
    public Sprite Trap;
    public AudioClip �ƶ�;
    public AudioClip �����;
    public AudioClip ��������;
    public GameObject ���Ԥ�Ƽ�;

    private HashSet<GameObject> NearBlock;
    private Material LastMaterial;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        DetectNear();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<GameObject>()!=null&&!FindObjectOfType<GameManager>().stop) 
        {
            Vector2 moveDir;
            moveDir = DetectKey();
            if (moveDir != Vector2.zero)
            {
                if (CanMove(moveDir))
                {
                    Move(moveDir);
                    DetectNear();
                }
            }
            DetectBreak();
            moveDir = Vector2.zero;
        }
    }

    //��ⰴ��
    private Vector2 DetectKey()
    {
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveDir = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveDir = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveDir = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveDir = Vector2.down;
        }
        return moveDir;
    }

    //�ƶ�
    private void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }

    //����Ƿ����ϰ��谭�ƶ�
    private bool CanMove(Vector2 dir)
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position, dir,1.0f,RoadMask);
        if (!hit)
        {
            source.clip = �ƶ�;
            source.PlayOneShot(source.clip);
            return true;
        }
        else
        {
            if (hit.collider.GetComponent<BoxController>() != null)//�ϰ���������
            {
                return hit.collider.gameObject.GetComponent<BoxController>().CanMove(dir);
            }
            else
            {
                source.clip = �ƶ�;
                source.PlayOneShot(source.clip);
                return false;
            }
            }
        }
    //����������������ӡ��ui��
    private void DetectNear()
    {
        NearBlock = new HashSet<GameObject>();
        int mine = 0;
        RaycastHit2D hit;
        for(int i = -1; i <= 1; i++)
        {
            for(int j=-1; j <= 1; j++)
            {
                Vector2 dir=new Vector2(i,j);
                hit = Physics2D.Raycast(transform.position, dir, 1.0f, RoadMask);
                if(hit)
                {
                    GameObject obj=hit.collider.gameObject;
                    if (obj.CompareTag("Landmine")) mine++;
                    if (i == 0 || j == 0)//�����������
                    {
                        if (!obj.CompareTag("Barrier") && !obj.CompareTag("Box"))
                            NearBlock.Add(obj);
                    }
                }
                
            }
        }
        ShowMine(mine);
    }
    private void ShowMine(int mine)
    {
        GameObject landmine = GameObject.Find("UI").transform.Find("Landmine").Find("Num").gameObject;
        Text text=landmine.GetComponent<Text>();
        if(text != null)
        {
            text.text = mine.ToString();
        }
    }

    //��������ƻ�����
    private void DetectBreak()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
        if (hit)
        {
            GameObject obj=hit.collider.gameObject;
            if (NearBlock.Contains(obj) && !FindObjectOfType<GameManager>().setting)//������Ե㣬���Ƹ���
            {
                Material material = obj.GetComponent<SpriteRenderer>().material;
                if (LastMaterial!=null&& material!=LastMaterial && LastMaterial.GetFloat("_Thickness") == THICKNESS)
                    LastMaterial.SetFloat("_Thickness", 0);
                if (material.GetFloat("_Thickness") == 0)
                {
                    LastMaterial = material;
                    LastMaterial.SetFloat("_Thickness", THICKNESS);
                }
            }
            if (Input.GetMouseButtonDown(0) && !FindObjectOfType<GameManager>().setting)//����������
            {
                if (NearBlock.Contains(obj))//����˿��������ϰ���
                {
                    if (obj.CompareTag("Block"))
                    {
                        Destroy(obj);
                        source.clip = �����;
                        source.PlayOneShot(source.clip);
                        NearBlock.Remove(obj);
                    }
                    if (obj.CompareTag("Landmine"))
                    {
                        if(obj.transform.parent.GetComponent<SpriteRenderer>().sprite == null)
                        {
                            obj.transform.parent.GetComponent<SpriteRenderer>().sprite = Trap;
                        }
                        source.clip = ��������;
                        source.PlayOneShot(source.clip);
                        FindObjectOfType<GameManager>().Lose(obj);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && !FindObjectOfType<GameManager>().setting)
            {
                if (obj.transform.Find("Mark")==null)
                {
                    GameObject prefab = Instantiate(���Ԥ�Ƽ�, obj.transform);
                    prefab.name = "Mark";
                    prefab.transform.parent = obj.transform;
                }
                else
                {
                    Destroy (obj.transform.Find("Mark").gameObject);
                }
            }
        }
        else
        {
            if(LastMaterial!=null)
                LastMaterial.SetFloat("_Thickness", 0);
        }
    }
}
