using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class AutoRotate : MonoBehaviour {

public Vector3 rotVector = Vector3.zero;
	
	void Update () {
		transform.Rotate(rotVector * Time.deltaTime);
	}
}
