using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public AudioClip 推箱子;
    public AudioClip 通关;

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
            GameObject child = transform.Find("笼子").gameObject;
            Destroy(child);
            source.clip = 通关;
            source.PlayOneShot(source.clip);
            FindObjectOfType<GameManager>().Win();
        }
    }
    private void Move(Vector2 dir)
    {
        transform.Translate(dir);
        source.clip = 推箱子;
        source.PlayOneShot(source.clip);
    }
}
