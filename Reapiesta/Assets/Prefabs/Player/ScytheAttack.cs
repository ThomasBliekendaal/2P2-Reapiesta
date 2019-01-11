using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{

    PlayerController plyr;
    [SerializeField]
    GameObject hurtBox;

    bool firstAtkFrame = false;

    void Start()
    {
        plyr = FindObjectOfType<PlayerController>();
        hurtBox.SetActive(false);
    }

    void Update()
    {
        if (plyr.pf.curState != PlayerFunctions.State.Dash)
        {
            if (plyr.pf.curState != PlayerFunctions.State.Attack)
            {
                if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == false)
                {
                    if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackStart") == false)
                    {
                        Attack();
                    }
                    else
                    {
                        BufferEndAttackValues();
                    }
                }
                else
                {
                    AttackStuff();
                }
            }
            else
            {
                AttackStuff();
            }
        }
        else
        {
            firstAtkFrame = false;
            plyr.pf.anim.SetBool("KeepAttacking", false);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack") && plyr.pf.curState == PlayerFunctions.State.Foot)
        {
            StaticFunctions.PlayAudio(13, false);
            plyr.pf.curState = PlayerFunctions.State.Attack;
            plyr.pf.anim.Play("AttackStart", 0);
            firstAtkFrame = false;
            plyr.pf.anim.SetBool("KeepAttacking", false);
        }
    }

    void AttackStuff()
    {
        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackStart") == true)
        {
            if (firstAtkFrame == false)
            {
                firstAtkFrame = true;
                plyr.pf.anim.SetBool("KeepAttacking", false);
            }
            if (Input.GetButtonDown("Attack"))
            {
                plyr.pf.anim.SetBool("KeepAttacking", true);
            }
        }
        else
        {
            // plyr.pf.anim.SetBool("KeepAttacking", false);
            firstAtkFrame = false;
        }


        //plyr.pf.cc.Move(plyr.transform.forward * Time.deltaTime * 10);
        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            hurtBox.SetActive(true);
            plyr.pf.cc.Move(plyr.transform.forward * Time.deltaTime * 20);
        }
        else
        {
            hurtBox.SetActive(false);
        }


        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") == true && Input.GetButtonDown("Attack") == false)
        {
            hurtBox.SetActive(false);
            plyr.pf.curState = PlayerFunctions.State.Foot;
            plyr.pf.moveV3 = new Vector3(0, plyr.pf.moveV3.y, 0);
        }
        else if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == true && Input.GetButtonDown("Attack") == false)
        {
            hurtBox.SetActive(false);
            plyr.pf.curState = PlayerFunctions.State.Foot;
            plyr.pf.moveV3 = new Vector3(0, plyr.pf.moveV3.y, 0) + (plyr.transform.forward * Vector3.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 2));
        }

        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == true && Input.GetButtonDown("Attack"))
        {
            plyr.pf.anim.SetBool("KeepAttacking", true);
            plyr.pf.curState = PlayerFunctions.State.Attack;
            firstAtkFrame = false;
            Invoke("BufferEndAttackValues", 0.2f);
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > -0.0001f)
        {
            plyr.transform.eulerAngles = new Vector3(plyr.transform.eulerAngles.x, plyr.pf.cam.transform.eulerAngles.y, plyr.transform.eulerAngles.z);
        }
    }

    void BufferEndAttackValues()
    {
        if (plyr.pf.curState != PlayerFunctions.State.Dash)
        {
            plyr.pf.curState = PlayerFunctions.State.Attack;
            firstAtkFrame = false;
        }
    }

}

