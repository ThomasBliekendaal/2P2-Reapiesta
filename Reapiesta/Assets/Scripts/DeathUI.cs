using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour {

RectTransform rect;
[SerializeField] float scaleSpeed = 0.5f;
	void Start () {
		rect = GetComponent<RectTransform>();
	}
	
	void Update () {
		rect.localScale = Vector3.MoveTowards(rect.localScale,Vector3.one * 200,Time.unscaledDeltaTime * scaleSpeed);
	}
}
