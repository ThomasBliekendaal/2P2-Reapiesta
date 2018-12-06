using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOptions : MenuButton {

    [SerializeField] int newState = 1;

	void Start()
    {
        StartStuff();
    }

    void Update()
    {
        UpdateStuff();
    }

    public override void ClickEvent()
    {
        Invoke("EventStuff",1);
		FindObjectOfType<MenuManager>().curState = newState;
    }

    void EventStuff()
    {
       // StaticFunctions.LoadScene(0);
    }
}
