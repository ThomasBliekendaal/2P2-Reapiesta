using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float range = 100;
    PlayerController plyr;
    Camera cam;
    [SerializeField] float slowMotionSpeed = 0.3f;
    void Start()
    {
        plyr = FindObjectOfType<PlayerController>();
        cam = Camera.main;
    }

    void Update()
    {
        Controll();
    }

    void Controll()
    {
        if (Input.GetButtonUp("Attack"))
        {
            ShootNow();
            Time.timeScale = 1;
        }
        if (Input.GetAxis("Attack") == 0)
        {
            //cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 60, Time.deltaTime * 150);
            Time.timeScale = 1;
        }
        else
        {
            if (plyr.pf.cc.isGrounded == false && plyr.pf.curState != PlayerFunctions.State.SkateBoard)
            {
                Time.timeScale = slowMotionSpeed;
                // cam.fieldOfView = 40;
            }
            else if (plyr.pf.curState == PlayerFunctions.State.SkateBoard && plyr.pf.grounded == false && plyr.pf.cc.isGrounded == false)
            {
                Time.timeScale = slowMotionSpeed;
            }
            else
            {
                Time.timeScale = 1;
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
                cam.GetComponent<Cam>().MediumShake();
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
