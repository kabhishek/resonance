using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

     public float translateX = 2f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

          gameObject.transform.position += new Vector3 (translateX * Time.fixedDeltaTime, 0, 0);
		
	}
}
