using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {
	public uint lives = 10;

	void Awake() {
		lives = SaveLoad.LoadManager();
	}
}
