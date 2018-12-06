using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMLoadGame : MenuButton {

	[SerializeField] GameObject loadingObj;
    [SerializeField] Text loadingText;
    [SerializeField] GameObject loadingCircle;

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
        loadingObj.SetActive(true);
    }

    void EventStuff()
    {
        StartCoroutine(SceneLoader());

    }

    IEnumerator SceneLoader()
    {
        //it loads in the background, then when you click, insta load!
        AsyncOperation async = SceneManager.LoadSceneAsync(0);
        while (!async.isDone)
        {
            if (Input.GetButtonDown("Attack") == true)
            {
                async.allowSceneActivation = true;
            }
            else
            {
                async.allowSceneActivation = false;
                if (loadingCircle.activeSelf == true)
                {
                    loadingText.text = "Press to continue.";
                    loadingCircle.SetActive(false);
                }
            }
            yield return null;
        }
    }
}
