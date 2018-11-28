using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] float curHealth = 3;
    public int team = 0;
    [SerializeField] GameObject deathParticle;
    [SerializeField] GameObject hitParticle;
    [Header("ScreenShake")]
    [SerializeField] bool hitShake = false;
    [SerializeField] bool dieShake = false;
    Cam cam;
    [SerializeField] UIPercentBar uiBar;
    float maxHealth = 1;

    void Start()
    {
        cam = Camera.main.GetComponent<Cam>();
        maxHealth = curHealth;
    }
    public virtual void Hit(float damage)
    {
        curHealth -= damage;
        //Debug.Log(curHealth + " health left.");
        if (curHealth <= 0)
        {
            Die();
        }
        else if (hitShake == true)
        {
            cam.SmallShake();
            if (hitParticle != null)
            {
                Instantiate(hitParticle, transform.position, transform.rotation);
            }
        }
        //sets the ui bar if there is one
        if (uiBar != null)
        {
            uiBar.curPercent = curHealth / maxHealth * 100;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        StaticFunctions.PlayAudio(2);
        //Debug.Log(name + " died");
        if (dieShake == true)
        {
            cam.MediumShake();
        }
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
        }
    }
}
