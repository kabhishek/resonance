using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour, IWaveInfo, ICollisionInfo {

	public CollisionEvent PlayerCollision 
	{
		get 
		{
			return playerCollision;
		}
		set
		{ 
		}
	}

	public WaveTrailRenderer WaveTrailRenderer 
	{
		get 
		{
			return gameObject.GetComponentInChildren<WaveTrailRenderer> ();
		}
		set
		{
		}
	}

	private void OnTriggerEnter2D(Collider2D collider2D)
	{
		float omega = collider2D.gameObject.GetComponent<NPC>().omega;
		collider2D.gameObject.SetActive (false);
		playerCollision.Invoke(omega, this);
	}

	private List<float> wavePoints;
	private CollisionEvent playerCollision = new CollisionEvent();
}
