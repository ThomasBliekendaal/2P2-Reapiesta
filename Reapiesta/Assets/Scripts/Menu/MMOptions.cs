using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOptions : MenuButton {

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
		Debug.Log("eh, yeah?");
    }

    void EventStuff()
    {
        StaticFunctions.LoadScene(0);
    }
}
