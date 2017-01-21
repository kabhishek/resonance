using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour, IWaveInfo {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collider2D)
	{
		Debug.Log ("Change Wave!!");
		float omega = collider2D.gameObject.GetComponent<NPC>().omega;
		gameObject.GetComponentInChildren<WaveTrailRenderer> ().curveStep = omega * -10.0f;
		collider2D.gameObject.SetActive (false);
	}

	public List<float> WavePoints
	{
		get {
			return wavePoints;
		}
		set {
			wavePoints = value;
		}
	}

	private List<float> wavePoints;
		
}
