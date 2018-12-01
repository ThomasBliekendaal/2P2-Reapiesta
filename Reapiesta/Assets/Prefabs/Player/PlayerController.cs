﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [HideInInspector] public PlayerFunctions pf;

    void Start()
    {
        pf = GetComponent<PlayerFunctions>();
    }
    void Update()
    {
        Controll();
    }

    void Controll()
    {
        switch (pf.curState)
        {

            case PlayerFunctions.State.Foot:
                pf.grounded = true;
                pf.skateBoard.SetActive(false);
                pf.MoveForward();
                pf.AngleY();
                pf.Gravity();
                pf.FinalMove();
                if (Input.GetButtonDown("Fire2"))
                {
                    pf.StartSkateBoard();
                }
                if (Input.GetButtonDown("Dash"))
                {
                    pf.StartDash();
                }
                if (pf.cc.isGrounded == true)
                {
                    if(pf.dustParticles.isStopped == true){
                    pf.dustParticles.Play();
                    }
                }
                else
                {
                     pf.dustParticles.Stop();
                }
                break;
            case PlayerFunctions.State.SkateBoard:
                pf.skateBoard.SetActive(true);
                pf.dustParticles.Stop();
                pf.SkateForward();
                pf.SkateAngleY();
                // Gravity();
                pf.FinalMove();
                if (Input.GetButtonDown("Fire2"))
                {
                    StaticFunctions.PlayAudio(1, false);
                    pf.curState = PlayerFunctions.State.Foot;
                    transform.eulerAngles = new Vector3(0,Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y,0);
                    Instantiate(pf.particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                    pf.cam.MediumShake();

                    if (pf.canSkateJump == false)
                    {
                        pf.moveV3 = new Vector3(pf.moveV3.x, pf.jumpHeight, pf.moveV3.z);
                    }
                }
                if (Input.GetButtonDown("Dash") && pf.stamina > 30)
                {
                    pf.stamina -= 30;
                    pf.StartDash();
                }
                break;
            case PlayerFunctions.State.Dash:
                pf.dustParticles.Stop();
                pf.DashStuff();
                pf.FinalMove();
                break;
        }
        pf.staminaBar.curPercent = pf.stamina;
        pf.stamina = Mathf.MoveTowards(pf.stamina, 100, Time.deltaTime * 10);
    }


}
