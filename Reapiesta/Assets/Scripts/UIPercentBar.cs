using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPercentBar : MonoBehaviour {

float maxScale;
public float curPercent = 100;
RectTransform rect;
[SerializeField] float lerpSpeed = 1;

	void Start () {
		rect = GetComponent<RectTransform>();
		maxScale = rect.localScale.x;
	}
	
	void Update () {
		rect.localScale = new Vector3(Mathf.Lerp(rect.localScale.x ,maxScale * (curPercent / 100),lerpSpeed * Time.unscaledDeltaTime),rect.localScale.y,rect.localScale.z);
	}
}
