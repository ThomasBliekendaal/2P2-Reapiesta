using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Hurtbox : MonoBehaviour
{

    [SerializeField] float damage = 1;
    [SerializeField] int team = 0;

    void OnTriggerEnter(Collider other)
    {
        Hitbox hit = other.GetComponent<Hitbox>();
        if (hit != null)
        {
            if (team != hit.team)
            {
                hit.Hit(damage);
              // Debug.Log(other.name + " got hit!");
            }
            else
            {
              //  Debug.Log("Same team");
            }
        }
    }
}
