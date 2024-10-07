using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Load2 : MonoBehaviour
{
    public float ShowTime = 2.0f;
    public GameObject BGM;
    SpriteRenderer bg;
    private float time = 0;
    private float slerp;
    // Start is called before the first frame update
    void Start()
    {
        bg=GetComponent<SpriteRenderer>();
        slerp = 1/ShowTime;
    }
    private void OnEnable()
    {
        Destroy(BGM);
    }
    // Update is called once per frame
    void Update()
    {
        if(time <= ShowTime)
        {
            time += Time.deltaTime;
            bg.color = new Color(bg.color.r,bg.color.g,bg.color.b,time*slerp);
        }
        if (time * slerp >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
