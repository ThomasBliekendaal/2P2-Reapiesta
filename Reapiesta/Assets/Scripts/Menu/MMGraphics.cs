using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMGraphics : MenuButton
{

    [SerializeField] bool add = true;
    [SerializeField] Text ntxt;
    // [SerializeField] Vector2Int newResolution = new Vector2Int(1920, 1080);

    void Start()
    {
        StartStuff();
    }

    void Update()
    {
        UpdateStuff();
		ntxt.text = QualitySettings.names[QualitySettings.GetQualityLevel()] + "";
    }

    public override void ClickEvent()
    {
        Invoke("EventStuff", 1);
        //QualitySettings.SetQualityLevel(newLevel,true);
        if (add == true)
        {
            if (QualitySettings.GetQualityLevel() < QualitySettings.names.Length)
            {
                QualitySettings.IncreaseLevel(true);
            }
        }
        else if (QualitySettings.GetQualityLevel() > 0)
        {
            QualitySettings.DecreaseLevel(true);
        }
        //Screen.SetResolution(newResolution.x, newResolution.y, Screen.fullScreenMode, 60);
    }

    void EventStuff()
    {
        // StaticFunctions.LoadScene(0);
    }
}
