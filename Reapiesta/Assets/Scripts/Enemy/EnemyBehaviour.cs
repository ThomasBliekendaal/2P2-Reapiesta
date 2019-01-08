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
    GameObject sensefield;
    LayerMask mask;
    void Start()
    {
        mask = baseStats.layer.value;
        viewRadius = baseStats.viewRadius;
        transform.tag = baseStats.tag;
        GetComponent<Ground>().target = target;
        if (baseStats.enemyAttack != EnemyAttack.melee)
        {
            GetComponent<Range>().target = target;
        }
        else
        {
            GetComponent<Melee>().target = target;
        }
        Sensefield();
    }
    void Sensefield()
    {
        sensefield = new GameObject();
        Transform field = Instantiate(sensefield, transform.position, transform.rotation).transform;
        field.SetParent(transform);
        field.gameObject.AddComponent(typeof(SphereCollider));
        field.gameObject.AddComponent(typeof(SensField));
        print(mask.value);
        field.gameObject.layer = mask;
        field.tag = baseStats.tag;
        field.GetComponent<SphereCollider>().isTrigger = baseStats.isTrigger;
        field.GetComponent<SphereCollider>().radius = viewRadius;
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
