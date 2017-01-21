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
     float omegaX = 1.0f;
     float omegaY = 1.0f;
     float index;
	// Use this for initialization
	void Start () {

          rb = gameObject.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void FixedUpdate()
     {
          theta += thetaStep;
          float localX = transform.position.x;
		  localX += Time.fixedDeltaTime /* speed*/;
          //float localY = transform.position.y;
          //thetaStep *= Time.fixedDeltaTime;
          //localY += Mathf.Sin (theta) * speed;
          //transform.position = new Vector2 (localX, localY);



          /*theta += thetaStep;
          Vector3 referenceVector = new Vector2 (0.01f, 
               Mathf.Sin (theta) * 0.01f);
          rb.velocity = referenceVector.normalized * Time.fixedDeltaTime * speed;*/
          //rb.angularVelocity = 20.0f;



		  index += Time.fixedDeltaTime;

          //float x = amplitudeX*Mathf.Cos (omegaX*index);
          float y = amplitudeY * Mathf.Sin (omegaY * index);
          //wave_trail//transform.position= new Vector3(localX,transform.position.y);
          transform.position= new Vector3(localX,y) * speed;
     }
}
