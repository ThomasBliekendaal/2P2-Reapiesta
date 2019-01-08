using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class GeneralEnemyCode : MonoBehaviour
{
    public float currentTime;
    public bool action;
    public float targetDist;
	public Animator anim;

    public virtual void Start()
    {
		anim = GetComponent<Animator>();
        //empty
    }

    public virtual void Update()
    {
        //empty
    }

    public virtual void CheckDist(Vector3 target, float minDist, MoveState state)
    {
        // check the distance of your target
        targetDist = Vector3.Distance(transform.position, target);
        if (targetDist < minDist && state == MoveState.chasing)
        {
            action = true;
            GetComponent<Ground>().groundAgent.isStopped = true;
            // when your distance is close enough stop 
            // change state to attcking
        }
        else
        {
            action = false;
            GetComponent<Ground>().groundAgent.isStopped = false;
        }
    }
    public virtual void Timer(float time)
    {
        if (action)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = time;
                Action();
            }
        }
    }
    public virtual void Action()
    {
    }
}
