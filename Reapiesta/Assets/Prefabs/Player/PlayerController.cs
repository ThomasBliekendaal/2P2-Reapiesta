using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

[HideInInspector] public PlayerFunctions pf;

void Start(){
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
                break;
            case PlayerFunctions.State.SkateBoard:
                pf.skateBoard.SetActive(true);
                pf.SkateForward();
                pf.SkateAngleY();
                // Gravity();
                pf.FinalMove();
                if (Input.GetButtonDown("Fire2"))
                {
                    pf.curState = PlayerFunctions.State.Foot;
                    transform.eulerAngles = Vector3.zero;
                    Instantiate(pf.particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                    pf.cam.MediumShake();

                    if (pf.canSkateJump == false)
                    {
                        pf.moveV3 += new Vector3(0, pf.jumpHeight, 0);
                    }
                }
                if (Input.GetButtonDown("Dash"))
                {
                    pf.StartDash();
                }
                break;
            case PlayerFunctions.State.Dash:
                pf.DashStuff();
                pf.FinalMove();
                break;
        }
    }

    
}
