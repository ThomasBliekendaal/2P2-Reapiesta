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
    public Vector3 target;
    bool trigger;
    public float currentTime;
    float viewRadius;
    void Start()
    {
        GetComponent<Ground>().target = target;
        GetComponent<Range>().target = target;
        viewRadius = baseStats.viewRadius;
        GetComponent<SphereCollider>().isTrigger = baseStats.isTrigger;
        GetComponent<SphereCollider>().radius = viewRadius;
        transform.tag = baseStats.tag;
    }

    void Update()
    {

    }

    void Timer()
    {

    }

    void Event()
    {
        // trigger if the swarm begins or if he is chasing the player down
    }
}
