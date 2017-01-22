using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private StateHandler stateHandler;
	[SerializeField] private GameObject canvasStory;
	private Color bgColor;

	// Use this for initialization
	void Awake ()
	{
		stateHandler.updateStateEvent.AddListener (UpdateStory);
	}
	// Use this for initialization
	void Start () {
		bgColor = new Color ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateStory(PlayerState playerState, PlayerState previousState, float state)
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
		UpdateEnvironment (state);
	}

	private void OnDestroy()
	{
		stateHandler.updateStateEvent.RemoveListener (UpdateStory);
	}

	void UpdateEnvironment(float state)
	{
		switch ((int)state) 
		{
			case 10:
			case -10:
				ColorUtility.TryParseHtmlString ("#48489AFF", out bgColor);
				break;
			case 30:
			case -30:
				ColorUtility.TryParseHtmlString ("#B82727FF", out bgColor);
				break;
			default:
				ColorUtility.TryParseHtmlString ("#424242FF", out bgColor);
				break;
		}
		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().color = bgColor;
		GameObject.Find("Background(Clone)").GetComponent<SpriteRenderer> ().color = bgColor;
	}
}
