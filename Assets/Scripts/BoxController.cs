using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public AudioClip ������;
    public AudioClip ͨ��;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        string flag = "Lilac{h1dden_fl4g_r3quires_f0r_r3v3rs3_eng1n33ring}";
        Debug.Log(flag);
        source =GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CanMove(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f);
        if (!hit|| hit.collider.gameObject.CompareTag("Door"))
        {
            Move(dir);
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            GameObject child = transform.Find("����").gameObject;
            Destroy(child);
            source.clip = ͨ��;
            source.PlayOneShot(source.clip);
            FindObjectOfType<GameManager>().Win();
        }
    }
    private void Move(Vector2 dir)
    {
        transform.Translate(dir);
        source.clip = ������;
        source.PlayOneShot(source.clip);
    }
}
