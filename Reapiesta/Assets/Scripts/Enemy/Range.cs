using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Range : GeneralEnemyCode
{
    Transform player;
    public Vector3 target;
    float mintargetDist;
    [SerializeField] float forceAmount;
    public RangeStats rangeStats;
    public override void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTime = rangeStats.attackSpeed;
        mintargetDist = rangeStats.mintargetDist;
        forceAmount = rangeStats.forceAmount;
    }
    public override void Update()
    {
        Timer(rangeStats.attackSpeed);
        CheckDist(target, targetDist, GetComponent<Ground>().moveState);
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
            Timer(rangeStats.attackSpeed);
        }
    }

    public override void Action()
    {
        Transform newBottle = Instantiate(rangeStats.bottle, transform.position, transform.rotation);
        Rigidbody addRigid = newBottle.GetComponent<Rigidbody>();
        addRigid.velocity = (target - transform.position).normalized * forceAmount;
        addRigid.rotation = Quaternion.LookRotation(addRigid.velocity);
    }

}
