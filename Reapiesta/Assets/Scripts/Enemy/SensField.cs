using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensField : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (transform.parent.gameObject.GetComponent<Melee>())
            {
                transform.parent.gameObject.GetComponent<Melee>().target = other.transform.position;
            }
            else
            {
                transform.parent.gameObject.GetComponent<Range>().target = other.transform.position;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (transform.parent.gameObject.GetComponent<Melee>())
            {
                transform.parent.gameObject.GetComponent<Melee>().target = other.transform.position;
            }
            else
            {
                transform.parent.gameObject.GetComponent<Range>().target = other.transform.position;
            }
        }
    }
}
