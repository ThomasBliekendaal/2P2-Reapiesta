﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSwitch : MonoBehaviour
{

    public int curItem = 0;
    [SerializeField] Text ui;
    [SerializeField] ScytheThrow special;
    [SerializeField] int specialDisable = 0;

    void Start()
    {
        ui.text = transform.GetChild(curItem).name;
    }

    void Update()
    {
        Scroll();
        SetActives();
        ActivateSpecial();
    }

    void SetActives()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == curItem)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void Scroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            curItem += (int)Input.mouseScrollDelta.y;
            // if(Input.anyKeyDown == true){
            curItem += (int)Input.GetAxis("ScrollItem");
            //  }
            if (curItem > transform.childCount - 1)
            {
                curItem = 0;
            }
            if (curItem < 0)
            {
                curItem = transform.childCount - 1;
            }
            ui.text = transform.GetChild(curItem).name;
            Time.timeScale = 1;
        }
    }

    void ActivateSpecial()
    {
        if (curItem == specialDisable && Input.GetButtonDown("Throw"))
        {
            // special.SetActive(false);
            curItem++;
        }
        if (curItem == specialDisable && special.curState != ScytheThrow.State.Disabled)
        {
            Scroll();
        }
    }
}
