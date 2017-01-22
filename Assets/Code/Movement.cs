using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

     private Rigidbody2D rb;
     private float theta = 0f;
     private float thetaStep = 0.5f;
     public float speed = 20f;
     float amplitudeX = 10.0f;
     float amplitudeY = 1.0f;
     float omegaX;
     float omegaY = 1.0f;
     float index;
	float startY;


	// Use this for initialization
	void Start () {
			
 		rb = gameObject.GetComponent<Rigidbody2D> ();
		NPC npc = gameObject.GetComponent<NPC> ();
		omegaY = npc.omega;
		startY = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void FixedUpdate()
     {
		theta += thetaStep;
		float localX = transform.position.x;
		localX -= Time.fixedDeltaTime /* speed*/;

		index += Time.fixedDeltaTime;

		float y = amplitudeY * Mathf.Sin (omegaY * index);
		transform.position= new Vector3(localX * speed, y + startY) * speed;//+ transform.position.y
     }
}
