using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Operation : MonoBehaviour
{
    public Sprite[] operations;
    public GameObject NextButton;
    public GameObject LastButton;

    private Image Tip;
    private int page = 0;
    private void Start()
    {
        Tip=transform.GetComponent<Image>();
        LastButton.SetActive(false);
        if (operations != null) Tip.sprite = operations[page];
    }
    public void NextPage()
    {
        Tip.sprite = operations[++page];
        Detect();
    }
    public void LastPage()
    {
        Tip.sprite = operations[--page];
        Detect();
    }
    private void Detect()
    {
        if (page >= operations.Length-1)
        {
            NextButton.SetActive(false);
        }
        else
        {
            NextButton.SetActive(true);
        }
        if (page <= 0)
        {
            LastButton.SetActive(false);
        }
        else
        {
            LastButton.SetActive(true);
        }
    }
}
