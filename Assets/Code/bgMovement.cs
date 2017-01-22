using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMovement : MonoBehaviour {

	public float speed;

	//private Vector3 startPosition;
	private Vector2 duplicateStartPosition;
	private float backgroundSize;

	// Use this for initialization
	void Start () {
		// temp fix for bg scroll gap issue
		backgroundSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x - 1;

		duplicateStartPosition = new Vector3(backgroundSize, transform.position.y, transform.position.z);
		if (GameObject.FindGameObjectsWithTag ("Background").Length < 2) {
			GameObject duplicate = Instantiate<GameObject> (gameObject);
			duplicate.transform.position = duplicateStartPosition;
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.left * Time.deltaTime * speed);

		// If it is out of bounds, destroy
		if (transform.position.x <= -backgroundSize) {
			transform.position = duplicateStartPosition;
		}
	}
}
