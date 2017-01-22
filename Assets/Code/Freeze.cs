using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	void OnEnable() {
		Time.timeScale = 0;
	}

	void OnDisable() {
		Time.timeScale = 1;
	}

	void Update() {
		if (gameObject.activeSelf) {
			if (Input.anyKey) {
				// close story
				gameObject.SetActive(false);
			}
		}
	}
}
