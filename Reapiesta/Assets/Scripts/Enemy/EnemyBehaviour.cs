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
    [HideInInspector] public BaseStats baseStats;
    [HideInInspector] public Vector3 target;
    float viewRadius;
    void Start()
    {
        if (GetComponent<Ground>())
        {
            GetComponent<Ground>().newStart(target);
        }else if (GetComponent<Flying>())
        {
            // set target to flying component
        }
        viewRadius = baseStats.viewRadius;
        GetComponent<SphereCollider>().isTrigger = baseStats.isTrigger;
        GetComponent<SphereCollider>().radius = viewRadius;
        transform.tag = baseStats.tag;
    }

    void Update()
    {

    }

    void Event()
    {
        // trigger if the swarm begins or if he is chasing the player down
    }
}
