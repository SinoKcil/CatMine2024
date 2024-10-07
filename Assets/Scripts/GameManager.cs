using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static string KeyLastDead = "LastDead";
    static string KeyLose = "LoseTimes";
    static string KeyStarted = "Started";

    public bool stop;
    public bool setting = false;
    public Sprite LastDead;
    public GameObject WinLoad;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Started")) PlayerPrefs.SetInt(KeyLose, 0);//没有启动过方法
        PlayerPrefs.SetInt("Started", 1);
        SceneManager.sceneLoaded += OnSceneLoaded;//挂载该方法
    }
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        else if (stop && Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            stop = false;
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //重新游玩
    private void Reset()
    {
        PlayerPrefs.SetInt(KeyLose, PlayerPrefs.GetInt(KeyLose) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Lose(GameObject dead)
    {
        dead.SetActive(false);
        PlayerPrefs.SetString(KeyLastDead, dead.name);
        stop = true;
        GameObject Lose = GameObject.Find("UI").transform.Find("Lose").gameObject;
        Lose.SetActive(true);
    }
    public void Win()
    {
        PlayerPrefs.DeleteAll();
        if (WinLoad != null) WinLoad.SetActive(true);
        else StartCoroutine(LoadNextLevel());
    }
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey(KeyLastDead))
        {
            GameObject Dead = GameObject.Find(PlayerPrefs.GetString(KeyLastDead));
            Dead.transform.parent.GetComponent<SpriteRenderer>().sprite = null;
            if (Dead != null)
            {
                SpriteRenderer renderer = Dead.GetComponent<SpriteRenderer>();
                if (renderer != null && LastDead != null)
                {
                    Vector2 pivot = new Vector2(renderer.sprite.pivot.x, renderer.sprite.pivot.y);
                    renderer.sprite = null;
                    renderer.sprite = LastDead;
                }
            }
        }
        int dead = 0;
        if(PlayerPrefs.HasKey(KeyLose)) dead = PlayerPrefs.GetInt(KeyLose);
        GameObject.Find("UI").transform.Find("LoseRecord").Find("Num").GetComponent<Text>().text = dead.ToString();
    }
    public void SetSetting(bool value)
    {
        setting = value;
    }
}
