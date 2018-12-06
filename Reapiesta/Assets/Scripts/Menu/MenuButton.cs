using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [HideInInspector] public bool isOver = false;
    [SerializeField] string button = "Attack";
    [SerializeField] Text txt;
    [SerializeField] Image img;
    void Start()
    {
        StartStuff();
    }

    public void StartStuff()
    {
        if (GetComponent<Text>() != null)
        {
            txt = GetComponent<Text>();
        }
        if (GetComponent<Image>() != null)
        {
            img = GetComponent<Image>();
        }
    }

    void Update()
    {
        UpdateStuff();
    }

    public void UpdateStuff()
    {
        if (isOver == true && Input.GetButtonDown(button) == true)
        {
            ClickEvent();
        }
        if (Input.GetButtonDown(button) == true)
        {
            isOver = false;
        }
        else if (isOver == true)
        {
            HighLight();
        }
        else
        {
            DeEmphasize();
        }
    }

    public virtual void ClickEvent()
    {
        StaticFunctions.PlayAudio(0, false);
        StaticFunctions.LoadScene(1);
    }

    public virtual void HighLight()
    {
        if (txt != null)
        {
            txt.fontStyle = FontStyle.Bold;
        }
    }

    public virtual void DeEmphasize()
    {
        if (txt != null)
        {
            txt.fontStyle = FontStyle.Normal;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isOver = false;
    }
}
