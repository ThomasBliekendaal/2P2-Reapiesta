using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] float curHealth = 3;
    public int team = 0;
    public GameObject deathParticle;
    [SerializeField] GameObject hitParticle;
    [Header("ScreenShake")]
    [SerializeField] bool hitShake = false;
    public bool dieShake = false;
    [HideInInspector] public Cam cam;
    [SerializeField] UIPercentBar uiBar;
    float maxHealth = 1;
    [SerializeField] float stopTime = 0.01f;

    void Start()
    {
        StartStuff();
    }

    public void StartStuff()
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
            StaticFunctions.PlayAudio(8);
            //Debug.Log("hi");
            Time.timeScale = stopTime;
            StartCoroutine(SetTimeBack(stopTime * 10));
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

    IEnumerator SetTimeBack(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
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
