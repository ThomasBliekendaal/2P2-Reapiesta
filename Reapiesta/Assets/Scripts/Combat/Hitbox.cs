using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] float curHealth = 3;
    public int team = 0;
    [Header("ScreenShake")]
    [SerializeField] bool hitShake = false;
    [SerializeField] bool dieShake = false;
	Cam cam;

	void Start(){
		cam = Camera.main.GetComponent<Cam>();
	}
    public virtual void Hit(float damage)
    {
        curHealth -= damage;
        //Debug.Log(curHealth + " health left.");
        if (curHealth <= 0)
        {
            Die();
        } else if(hitShake == true){
			cam.SmallShake();
		}
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        //Debug.Log(name + " died");
		if(dieShake == true){
			cam.MediumShake();
		}
    }
}
