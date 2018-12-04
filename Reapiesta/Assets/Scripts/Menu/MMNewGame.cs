using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMNewGame : MenuButton
{

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
        Invoke("EventStuff", 1);
        Debug.Log("eh, yeah?");
    }

    void EventStuff()
    {
        //  StaticFunctions.LoadScene(0);
        StartCoroutine(SceneLoader());

    }

    IEnumerator SceneLoader()
    {
        //it loads in the background, then when you click, insta load!
        AsyncOperation async = SceneManager.LoadSceneAsync(0);
        while (!async.isDone)
        {
            Debug.Log(async.progress);
            if (Input.GetButtonDown("Attack") == true)
            {
                async.allowSceneActivation = true;
            }
            else
            {
                async.allowSceneActivation = false;
            }
            yield return null;
        }
    }
}
