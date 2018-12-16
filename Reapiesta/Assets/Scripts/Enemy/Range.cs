using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Range : MonoBehaviour
{
    Transform player;
    float targetDist;
    float mintargetDist;
    [SerializeField] float forceAmount;
    public RangeStats rangeStats;
    Vector3 target;
    float currentTime;
    bool throwing;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTime = rangeStats.attackSpeed;
        mintargetDist = rangeStats.mintargetDist;
        forceAmount = rangeStats.forceAmount;
    }
    void Update()
    {
        //Timer();
        CheckDist(target);
    }
    void CheckDist(Vector3 target)
    {
        // check the distance of your target
        targetDist = Vector3.Distance(transform.position, target);
        if (targetDist < mintargetDist)
        {
            throwing = true;
            GetComponent<Ground>().groundAgent.isStopped = true;
            // when your distance is close enough stop 
            // change state to attcking
        }
        else
        {
            throwing = false;
            GetComponent<Ground>().groundAgent.isStopped = false;
        }
    }

    void Timer()
    {
        if (throwing)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = rangeStats.attackSpeed;
                Throw();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            target = other.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            target = other.transform.position;
            Timer();
        }
    }

    void Throw()
    {
        Transform newBottle = Instantiate(rangeStats.bottle, transform.position, transform.rotation);
        Rigidbody addRigid = newBottle.GetComponent<Rigidbody>();
        addRigid.velocity = (target - transform.position).normalized * forceAmount;
        addRigid.rotation = Quaternion.LookRotation(addRigid.velocity);
    }

}
