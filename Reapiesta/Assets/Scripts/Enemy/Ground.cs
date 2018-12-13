using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum MoveState { idle, walking, chasing, attacking };
public class Ground : MonoBehaviour
{
    MoveState moveState;
    public GroundStats groundStats;
    public NavMeshAgent groundAgent;
    Vector3 target;
    void Start()
    {

    }
    public void newStart(Vector3 newtarget)
    {
        moveState = MoveState.idle;
        target = newtarget;
        groundAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Check();
    }

    void Check()
    {
        switch (moveState)
        {
            case MoveState.idle:
                // set the currentMovement to idle speed
                Idle();
                break;
            case MoveState.walking:
                Walking();
                // set the currentMovement speed to walking speed
                break;
            case MoveState.chasing:
                Walking();
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
            moveState = MoveState.walking;
            // set the target to the player
            target = other.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // set the moveState to idle
        moveState = MoveState.idle;
        // set the target to it self
        target = transform.position;
    }
}
