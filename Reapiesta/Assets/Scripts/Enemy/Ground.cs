using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ground : MonoBehaviour
{
    public MoveState moveState;
    public Vector3 target;
    public GroundStats groundStats;
    public NavMeshAgent groundAgent;
    void Start()
    {
        moveState = MoveState.idle;
        groundAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Check();
    }

    public void Check()
    {
        switch (moveState)
        {
            case MoveState.idle:
                // set the currentMovement to idle speed
                Idle();
                break;
            case MoveState.walking:
                Walking();
                groundAgent.speed = groundStats.walkSpeed;
                // set the currentMovement speed to walking speed
                break;
            case MoveState.chasing:
                Walking();
                groundAgent.speed = groundStats.runSpeed;
                //set currentMovement speed to running speed
                break;
        }
    }
    void Idle()
    {
        //set a enemy to a position
        groundAgent.SetDestination(target);
        //play an animation
    }
    void Walking()
    {
        groundAgent.SetDestination(target);
        // walk random around the area
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // set the moveState to running
            moveState = MoveState.chasing;
            // set the target to the player
            target = other.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // set the moveState to idle
            moveState = MoveState.idle;
            // set the target to it self
            target = transform.position;
        }
    }
}
