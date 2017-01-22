using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private StateHandler stateHandler;
	[SerializeField] private GameObject canvasStory;
	[SerializeField] private GameObject canvasObjective;

	private Color bgColor;
	private Color particleColor;

	// Use this for initialization
	void Awake ()
	{
		stateHandler.updateStateEvent.AddListener (UpdateStory);
	}
	// Use this for initialization
	void Start () {
		bgColor = new Color ();
		particleColor = new Color ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateStory(PlayerState playerState, PlayerState previousState)
	{
		Debug.Log ("PlayerState " + playerState);
		string story = string.Empty;
		string objective = string.Empty;

		switch (playerState) 
		{
			case PlayerState.Beginnings:	
			{
				story = "The beginning " + playerState.ToString ();
				objective = "First objective";
			}
			break;
		case PlayerState.Exploring:
			{
				story = "Explore your world " + playerState.ToString ();
			}
			break;
		case PlayerState.Calm:
			{
				story = "Now your are calm " + playerState.ToString ();
			}
			break;
		case PlayerState.Disturbed:
			{
				story = "Now you are disturbed " + playerState.ToString ();
			}
			break;
		case PlayerState.LonelyEnd:
			{
				story = "You have missunderstood her " + playerState.ToString ();
			}
			break;
		}

		if (story != string.Empty) 
		{
			canvasStory.SetActive (true);
			GameObject.FindGameObjectWithTag ("StoryText").GetComponent<Text> ().text = story;
		}

		if (objective != string.Empty) 
		{
			canvasObjective.SetActive (true);
			GameObject.FindGameObjectWithTag("ObjectiveText").GetComponent<Text> ().text = objective;
		}

		UpdateEnvironment (playerState);
	}

	private void OnDestroy()
	{
		stateHandler.updateStateEvent.RemoveListener (UpdateStory);
	}

	void UpdateEnvironment(PlayerState playerState)
	{
		switch (playerState) 
		{
		case PlayerState.Beginnings:
			ColorUtility.TryParseHtmlString ("#424242FF", out bgColor);
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out particleColor);
			break;
		case PlayerState.Lonely:
			ColorUtility.TryParseHtmlString ("#424242FF", out bgColor);
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out particleColor);
			break;
		case PlayerState.Exploring:
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out bgColor);
			ColorUtility.TryParseHtmlString ("#000000FF", out particleColor);
			break;
		case PlayerState.Calm:
			ColorUtility.TryParseHtmlString ("#48489AFF", out bgColor);
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out particleColor);
			break;
		case PlayerState.Disturbed:
			ColorUtility.TryParseHtmlString ("#B82727FF", out bgColor);
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out particleColor);
			break;
		case PlayerState.AnEnd:
			ColorUtility.TryParseHtmlString ("#424242FF", out bgColor);
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out particleColor);
			break;
		default:
			ColorUtility.TryParseHtmlString ("#FFFFFFFF", out bgColor);
			ColorUtility.TryParseHtmlString ("#000000FF", out particleColor);
			break;
		}

		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().color = bgColor;
		GameObject.Find("Background(Clone)").GetComponent<SpriteRenderer> ().color = bgColor;
		GameObject.Find("Particle").GetComponent<SpriteRenderer>().color = particleColor;
	}
}
