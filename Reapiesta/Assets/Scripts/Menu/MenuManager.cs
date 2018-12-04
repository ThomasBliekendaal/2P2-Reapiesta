using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public int curState = 0;
    public Objects[] gObjects;
    int lastState = 0;

    void Start()
    {
        lastState = curState;
    }

    void Update()
    {
        SetActives();
    }

    public void SetActives()
    {
        if (curState != lastState)
        {
            List<GameObject> ignoreHelper = new List<GameObject>();
            for (int i = 0; i < gObjects.Length; i++)
            {
                for (int i2 = 0; i2 < gObjects[i].gameObjects.Length; i2++)
                {
                    if (i == curState)
                    {
                        gObjects[i].gameObjects[i2].SetActive(true);
                        ignoreHelper.Add(gObjects[i].gameObjects[i2]);
                    }
                    else
                    {
                        //I save all objects I changed to ignoreHelper. If it was already modified, it shouldn't be modified again.
                        bool ignore = false;
                        if (ignoreHelper != null)
                        {
                            for (int i3 = 0; i3 < ignoreHelper.Count; i3++)
                            {
                                if (ignoreHelper != null && gObjects[i].gameObjects != null)
                                {
                                    if (ignoreHelper[i3] == gObjects[i].gameObjects[i2])
                                    {
                                        ignore = true;
                                    }
                                }
                            }
                        }
                        if (ignore == false)
                        {
                            gObjects[i].gameObjects[i2].SetActive(false);
                            ignoreHelper.Add(gObjects[i].gameObjects[i2]);
                        }
                    }
                }
            }
        }
        lastState = curState;
    }
}

[System.Serializable]
public class Objects
{
    public GameObject[] gameObjects;
}
