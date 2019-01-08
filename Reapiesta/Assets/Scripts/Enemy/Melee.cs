using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : GeneralEnemyCode
{
    public MeleeStats meleeStats;
    public Vector3 target;


    public override void Start()
    {
        base.Start();
        targetDist = meleeStats.mintargetDist;
        currentTime = meleeStats.attackSpeed;
    }

    public override void Update()
    {
        Timer(meleeStats.attackSpeed);
        CheckDist(target, targetDist, GetComponent<Ground>().moveState);
    }

    public override void Action()
    {
        if (action)
        {
            anim.SetBool("Action", true);
        }
        else
        {
            anim.SetBool("Action", false);
        }
    }
}
