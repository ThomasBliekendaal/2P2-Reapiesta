using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSwitch : MonoBehaviour
{

    public int curItem = 0;
    [SerializeField] Text ui;
    [SerializeField] ScytheThrow special;
    [SerializeField] int specialDisable = 0;
    bool scrollInUse = false;//this is used for controller input

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
        // if (Input.mouseScrollDelta.y != 0)
        // {
        int lastItem = curItem;
        curItem += (int)Input.mouseScrollDelta.y;
        if ((int)Input.GetAxis("ScrollItem") != 0)//the controller input
        {
            if (scrollInUse == false)
            {
                curItem += (int)Input.GetAxis("ScrollItem");
                scrollInUse = true;
            }
        }
        else
        {
            scrollInUse = false;
        }

        if (curItem != lastItem)
        {
            StaticFunctions.PlayAudio(0, false);
        }
        if (curItem > transform.childCount - 1)
        {
            curItem = 0;
        }
        if (curItem < 0)
        {
            curItem = transform.childCount - 1;
        }
        ui.text = transform.GetChild(curItem).name;
        if (Time.timeScale == 0.25f && StaticFunctions.paused == false)//EDIT THIS, IT'S HARDCODED
        {
            Time.timeScale = 1;
        }
        //}
    }

    void ActivateSpecial()
    {
        if (curItem == specialDisable && Input.GetButtonDown("Throw"))
        {
            // special.SetActive(false);
            curItem++;
            ui.text = transform.GetChild(curItem).name;
        }
        if (curItem == specialDisable && special.curState != ScytheThrow.State.Disabled)
        {
            Scroll();
            ui.text = transform.GetChild(curItem).name;
        }
    }
}
