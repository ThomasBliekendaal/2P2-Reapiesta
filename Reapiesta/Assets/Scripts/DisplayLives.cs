using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayLives : MonoBehaviour {

	Text txt;
	SaveData data;

	void Start(){
		txt = GetComponent<Text>();
		data = FindObjectOfType<SaveData>();
	}

	void Update(){
		txt.text = "X " + data.lives;
	}
}
