using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SlowMotion : MonoBehaviour
{

    PostProcessingBehaviour pp;

    void Start()
    {
        pp = Camera.main.GetComponent<PostProcessingBehaviour>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Time.timeScale = 0.2f;
            pp.profile.motionBlur.enabled = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Time.timeScale = 1f;
            pp.profile.motionBlur.enabled = false;
        }
    }
}
