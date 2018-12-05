using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [HideInInspector] public PlayerFunctions pf;
    [SerializeField] ItemSwitch itemSwitch;
    [SerializeField] Shoot shoot;
    [SerializeField] ScytheThrow scytheThrow;
    [SerializeField] ScytheAttack scytheAttack;
    PostProcessingBehaviour pp;

    void Start()
    {
        pf = GetComponent<PlayerFunctions>();
        pp = Camera.main.GetComponent<PostProcessingBehaviour>();
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
                // pp.profile.motionBlur.enabled = false;
                //here
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 2);
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
                    // pp.profile.motionBlur.enabled = true;
                    // Camera.main.fieldOfView = 40;
                    //here
                }
                if (pf.cc.isGrounded == true)
                {
                    if (pf.dustParticles.isStopped == true)
                    {
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
                    transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y, 0);
                    Instantiate(pf.particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                    pf.cam.MediumShake();

                    if (pf.canSkateJump == false)
                    {
                        pf.moveV3 = new Vector3(pf.moveV3.x, pf.jumpHeight, pf.moveV3.z);
                    }
                }
                if (Input.GetButtonDown("Dash") == true && pf.stamina > 10)
                {
                    pf.stamina -= 10;
                    if (pf.grounded == true)
                    {
                        pf.SkateBoost(true);
                        StaticFunctions.PlayAudio(13, false);
                        pp.profile.motionBlur.enabled = true;
                    }
                    else
                    {
                        pf.StartDash();
                    }
                    // pf.StartDash();
                }
                if (Input.GetAxis("Dash") != 0 && pf.stamina > 10 * Time.deltaTime)
                {
                    if (pf.grounded == true)
                    {
                        pf.SkateBoost(true);
                        pf.stamina -= 20 * Time.deltaTime;
                        pp.profile.motionBlur.enabled = true;
                    }
                }
                if (Input.GetButtonUp("Dash") == true || pf.grounded == false || pf.stamina < 10 * Time.deltaTime)
                {
                    pf.StopSkateBoost();
                    pp.profile.motionBlur.enabled = false;
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
