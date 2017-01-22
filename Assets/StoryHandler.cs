using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private StateHandler stateHandler;
	[SerializeField] private MonoBehaviour collisionInfo;
	[SerializeField] private GameObject canvasStory;
	private ICollisionInfo iCollisionInfo;
	// Use this for initialization
	void Awake ()
	{
		stateHandler.updateStateEvent.AddListener (UpdateStory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateStory(PlayerState playerState, PlayerState previousState)
	{
		Debug.Log ("PlayerState " + playerState);
		string story = string.Empty;
		switch (playerState) 
		{
			case PlayerState.Beginnings:	
			{
				story = "The beginning " + playerState.ToString ();
			}
			break;
		case PlayerState.AnEnd:
			{
				story = "It's sad to leave, this is the end " + playerState.ToString ();
			}
			break;
		}
		if (story != string.Empty) 
		{
			canvasStory.SetActive (true);
			GameObject.FindGameObjectWithTag ("StoryText").GetComponent<Text> ().text = story;
		}
	}

	private void OnDestroy()
	{
		stateHandler.updateStateEvent.RemoveListener (UpdateStory);
	}
}
