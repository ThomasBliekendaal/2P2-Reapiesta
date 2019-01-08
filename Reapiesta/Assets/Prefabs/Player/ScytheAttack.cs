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
        if (plyr.pf.curState != PlayerFunctions.State.Attack)
        {
            if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == false)
            {
                if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackStart") == false) {
                    Attack();
                } else
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
        //lastTag = plyr.pf.anim.GetCurrentAnimatorStateInfo(0).tagHash.ToString();
        //Debug.Log(plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackStart"));

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
            plyr.pf.cc.Move(plyr.transform.forward * Time.deltaTime * 10);
        }
        else
        {
            hurtBox.SetActive(false);
        }


        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle") == true && Input.GetButtonDown("Attack") == false)
        {
            hurtBox.SetActive(false);
            plyr.pf.curState = PlayerFunctions.State.Foot;
            plyr.pf.moveV3 = Vector3.zero;
        }
        else if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == true && Input.GetButtonDown("Attack") == false)
        {
            hurtBox.SetActive(false);
            plyr.pf.curState = PlayerFunctions.State.Foot;
            plyr.pf.moveV3 = Vector3.zero;
        }

        if (plyr.pf.anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackEnd") == true && Input.GetButtonDown("Attack"))
        {
            plyr.pf.anim.SetBool("KeepAttacking", true);
            plyr.pf.curState = PlayerFunctions.State.Attack;
            firstAtkFrame = false;
            Invoke("BufferEndAttackValues", 0.2f);
        }
    }

    void BufferEndAttackValues()
    {
        if (plyr.pf.curState == PlayerFunctions.State.Foot && plyr.GetComponent<Renderer>().enabled == true)
        {
            plyr.pf.curState = PlayerFunctions.State.Attack;
            firstAtkFrame = false;
        }
    }

}

