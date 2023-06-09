using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public List<DialogueItem> buttonItems = new List<DialogueItem>();
    public int select = -1;
    private float ScrollWheel;

    void Start()
    {
        buttonItems.Clear();
    }

    void Update()
    {
        if (buttonItems.Count > 0)
        {
            ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (ScrollWheel != 0)
            {
                print(ScrollWheel);
                select += (int)(ScrollWheel * 10);
                toSelectButton();    
            }

        }

        if (Input.GetKeyDown(KeyCode.F))
            buttonItems[select].dialogStart();
    }

    public void InitItem()
    {
        buttonItems = new List<DialogueItem>();
        buttonItems.Clear();
        select = -1;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (i > buttonItems.Count - 1)
            {
                buttonItems.Add(this.transform.GetChild(i).GetComponent<DialogueItem>());
            }
            else
            {
                buttonItems[i] = this.transform.GetChild(i).GetComponent<DialogueItem>();
            }
        }

        if (buttonItems.Count > 0)
        {
            select = 0;
            buttonItems[0].SelectImage.SetActive(true);
        }
    }

    void toSelectButton()
    {
        for (int i = 0; i < buttonItems.Count; i++)
        {
            buttonItems[i].SelectImage.SetActive(false);
        }

        if (select < 0)
        {
            select = 0;
            buttonItems[select].SelectImage.SetActive(true);
        }
        else if(select > buttonItems.Count -1)
        {
            select = buttonItems.Count - 1;
            buttonItems[select].SelectImage.SetActive(true);
        }
        else
        {
            buttonItems[select].SelectImage.SetActive(true);
        }
    }
}
