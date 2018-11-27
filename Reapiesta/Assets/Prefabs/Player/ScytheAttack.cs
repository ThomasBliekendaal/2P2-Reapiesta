using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{

    GameObject hitbox;
    Transform player;
	Cam cam;

    void Start()
    {
        hitbox = transform.GetChild(0).gameObject;
        player = FindObjectOfType<PlayerController>().transform;
        hitbox.transform.localRotation = Quaternion.Euler(0, 90, 0);
		cam = Camera.main.GetComponent<Cam>();
    }

    void Update()
    {
        transform.position = player.position;
        transform.eulerAngles = player.eulerAngles;
        Attack();
    }

    void Attack()
    {
        if (hitbox.activeSelf == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
				cam.SmallShake();
				hitbox.transform.localEulerAngles = new Vector3(0,180,0);
                hitbox.SetActive(true);
            }
        }
        else
        {
                hitbox.transform.localRotation = Quaternion.RotateTowards(hitbox.transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 1000);
                if (hitbox.transform.localRotation == Quaternion.Euler(0, 0, 0))
                {
                    hitbox.SetActive(false);
                }
            
        }
    }
}
