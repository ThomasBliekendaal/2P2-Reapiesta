using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Tip if you put this line of code above your class then it will at the component for you\\
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Hitbox))]
public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public EnemyStats baseStats;
    [HideInInspector] public Vector3 target;
    MoveState moveState;
    float viewRadius;
    void Start()
    {
        GetComponent<Ground>().newStart(target);
        viewRadius = baseStats.viewRadius;
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = viewRadius;
        moveState = MoveState.idle;
    }

    void Update()
    {

    }

    void Event()
    {
        // trigger if the swarm begins or if he is chasing the player down
    }
}
