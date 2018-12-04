using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class AutoRotate : MonoBehaviour {

public Vector3 tranformV3 = Vector3.zero;
public Vector3 eulerV3 = Vector3.zero;
public Vector3 scaleV3 = Vector3.zero;
public float speed = 1;

	void Start () {

	}
	
	void Update () {
		transform.position += tranformV3 * speed * Time.deltaTime;
		transform.Rotate(eulerV3 * speed * Time.deltaTime);
		transform.localScale += scaleV3 * speed * Time.deltaTime;
	}
}
