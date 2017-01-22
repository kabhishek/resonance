using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private MonoBehaviour collisionInfo;
	[SerializeField] private GameObject canvasStory;
	private ICollisionInfo iCollisionInfo; 
	// Use this for initialization
	void Start () {
		iCollisionInfo = collisionInfo.GetComponent<ICollisionInfo> ();
		iCollisionInfo.PlayerCollision.AddListener (UpdateStory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateStory(float state)
	{
		Debug.Log ("Story " + state);
		canvasStory.SetActive(true);
		GameObject.FindGameObjectWithTag ("StoryText").GetComponent<Text> ().text = "Lalal";
	}
		
}
