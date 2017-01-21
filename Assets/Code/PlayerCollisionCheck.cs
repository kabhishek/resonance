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
