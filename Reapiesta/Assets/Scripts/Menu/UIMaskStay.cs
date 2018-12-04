using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class UIMaskStay : MonoBehaviour
{

    Vector2 initialPos = Vector2.zero;
    Vector3 initialRot = Vector3.zero;
	
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.eulerAngles;
    }

    void LateUpdate()
    {
        transform.position = initialPos;
        transform.eulerAngles = initialRot;
    }
}
