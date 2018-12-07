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
	[Header("Partical")]
	[SerializeField] GameObject shootPartical;
    void Start()
    {
        currentAmmoAmount = Clipsize;
    }

    void Update()
    {
        Shoot();
    }

    void Reload()
    {
        if (Input.GetButtonDown("Reload"))
        {
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
            //  subtract bullet
            currentAmmoAmount--;
            //	instantiate bullet
            Transform newBullet = Instantiate(bullet);
            //	add force to the bullet
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.forward);
            UIFunction();
        }
    }

    void UIFunction()
    {
        // subtract bullet ammount
        ammoAmountText.text = currentAmmoAmount.ToString()+"/"+"Ammo";
    }
}
