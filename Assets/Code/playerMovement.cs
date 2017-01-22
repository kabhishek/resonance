﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour/*, IMovement*/ {

	public float speed;
	public GameObject canvasStory;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			// Particle goes up
			transform.Translate (Vector2.up * speed * Time.deltaTime);

		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			// Particle goes down
			transform.Translate (-Vector2.up * speed * Time.deltaTime);
		}
	}
}
