using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] float curHealth = 3;
    public int team = 0;
    [Header("ScreenShake")]
    bool hitShake = false;
    bool dieShake = false;
    public virtual void Hit(float damage)
    {
        curHealth -= damage;
        Debug.Log(curHealth + " health left.");
        if (curHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        Debug.Log(name + " died");
    }
}
