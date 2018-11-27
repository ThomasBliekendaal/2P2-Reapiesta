using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Shoot : MonoBehaviour
{

    public float range = 100;
    PlayerController plyr;
    Camera cam;
    PostProcessingBehaviour pp;
    [SerializeField] float slowMotionSpeed = 0.3f;
    void Start()
    {
        plyr = FindObjectOfType<PlayerController>();
        pp = Camera.main.GetComponent<PostProcessingBehaviour>();
        cam = Camera.main;
    }

    void Update()
    {
        Controll();
    }

    void Controll()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            ShootNow();
            Time.timeScale = 1;
            pp.profile.motionBlur.enabled = false;
        }
        if (Input.GetAxis("Fire1") == 0)
        {
            //cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 60, Time.deltaTime * 150);
            Time.timeScale = 1;
            pp.profile.motionBlur.enabled = false;
        }
        else
        {
            if (plyr.cc.isGrounded == false && plyr.curState != PlayerController.State.SkateBoard)
            {
                Time.timeScale = slowMotionSpeed;
                pp.profile.motionBlur.enabled = true;
                // cam.fieldOfView = 40;
            }
            else if (plyr.curState == PlayerController.State.SkateBoard && plyr.grounded == false && plyr.cc.isGrounded == false)
            {
                Time.timeScale = slowMotionSpeed;
                pp.profile.motionBlur.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                pp.profile.motionBlur.enabled = false;
            }
        }

    }

    void ShootNow()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                cam.GetComponent<Cam>().StartShake(0.2f, 0.5f);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
