using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWeapon : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] int Clipsize;
    [SerializeField] Text ammoAmountText;
    int currentAmmoAmount;
    [SerializeField] float forceAmount;
    [SerializeField] Transform bullet;
    [SerializeField] Transform barrelEnd;
    [Header("Partical")]
    [SerializeField] GameObject shootPartical;
    Transform camPos;
    RaycastHit hit;
    [SerializeField] float rayLenght;
    void Start()
    {
        camPos = Camera.main.transform;
        currentAmmoAmount = Clipsize;
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    void Reload()
    {
        if (Input.GetButtonDown("Reload"))
        {
            print("Reload");
            // set ammoAmount to max Ammo
            currentAmmoAmount = Clipsize;
            // trigger the UIFunction() for ammoUI
            UIFunction();
        }
    }

    void Shoot()
    {
        //	when click shoot
        if (Input.GetButtonDown("Attack"))
        {
            if (currentAmmoAmount > 0)
            {
                if (Physics.Raycast(camPos.position, camPos.forward, out hit, rayLenght))
                {
                    //  subtract bullet
                    currentAmmoAmount--;
                    //	instantiate bullet
                    Transform newBullet = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                    Rigidbody addRigid = newBullet.GetComponent<Rigidbody>();
                    //	add force to the bullet
                    addRigid.velocity = (hit.point - transform.position).normalized * forceAmount;
                    addRigid.rotation = Quaternion.LookRotation(addRigid.velocity);
                    UIFunction();
                }
                else
                {
                    //  subtract bullet
                    currentAmmoAmount--;
                    //	instantiate bullet
                    Transform newBullet = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                    Rigidbody addRigid = newBullet.GetComponent<Rigidbody>();
                    //	add force to the bullet
                    addRigid.velocity = (transform.forward - transform.position).normalized * forceAmount;
                    addRigid.rotation = Quaternion.LookRotation(addRigid.velocity);
                    UIFunction();
                }
            }
            if (currentAmmoAmount <= 0)
            {
                currentAmmoAmount = 0;
            }
        }
    }

    void UIFunction()
    {
        // subtract bullet ammount
        ammoAmountText.text = currentAmmoAmount + "/" + "Ammo".ToString();
    }
}
