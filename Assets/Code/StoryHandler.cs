using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private StateHandler stateHandler;
	[SerializeField] private GameObject canvasStory;
	[SerializeField] private GameObject canvasObjective;
	[SerializeField] private StoryData storyData;

	private Color bgColor;
	private Color particleColor;

	// Use this for initialization
	void Awake ()
	{
		stateHandler.updateStateEvent.AddListener (DelayedStoryUpdate);
	}
	// Use this for initialization
	void Start () {
		bgColor = new Color ();
		particleColor = new Color ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator DelayUpdateStory(PlayerState playerState, PlayerState previousState)
	{
		yield return new WaitForSecondsRealtime (storyData.delayToDisplayStory);
		UpdateStory (playerState, previousState);

	}

	private void DelayedStoryUpdate(PlayerState playerState, PlayerState previousState)
	{
		StartCoroutine (DelayUpdateStory (playerState, previousState));
	}

	private void UpdateStory(PlayerState playerState, PlayerState previousState)
	{
		Debug.Log ("PlayerState " + playerState);
		string[] story_possibilities = new string[] {string.Empty, string.Empty};
		string story = string.Empty;
		string objective = string.Empty;
		bool displayStory = true;

		switch (playerState) 
		{
		case PlayerState.Beginnings:	
			{
				story_possibilities[0] = "Please read to solve the poetry puzzles:\n\n\nShe was an empty scream\nDropped carelessly in a box of voices\nShe was powerful, wild yet furiously noiseless \nUnready to mingle with everyday sounds\n\nFor now, she craved to stay undisturbed\n\n\n(Press ENTER to continue)";
				story_possibilities [1] = "Please read to solve the poetry puzzles:\n\n\nThe air was swollen with quietude\nIt felt calmer than usual\n\n Vibrations of bygone conversations\nUsually lingering about\nWere nowhere\n\n It had no smell left\nno music, no story\n\n The air was empty but heavy\nwith the heart of an unwanted\n\nFor now, she craved to stay undisturbed\n\n\n(Press ENTER to continue)";
				objective = "For now, she craved to stay undisturbed";
			}
			break;
		case PlayerState.Exploring:
			{
				story_possibilities[0] = "Them:\n“These are cautiously pleated waves\nof us, of you.\nShall we?\nAs we resonate”\n\n\nShe meets, many a different waves\nAs she flows like a river";
				story_possibilities [1] = "Yesterdays continued to intertwine\nmoving along each other\nGravely.\n\n. Threads carrying whiffs and shadows\nlingered in a loci\n\nShe meets, many a different waves\n flowing like a river";
				objective = "She meets, many a different waves, flowing like a river";
			}
			break;
		case PlayerState.Calm:
			{
				story_possibilities[0] = "There were silhouettes of her conjunction\nto be sketched out\nShe paused to decorate her interval.\n\nThere would be iridescence when she turned around\nAn ornamental lapse would recite\nthe story of her placid travels\n\nAs she swam in tranquil lapse, an uneasiness took over\n\nShe had come too far without an emotion\nAll she desired, was a frantic endeavour";
				story_possibilities [1] = "There were silhouettes of her conjunction\nto be sketched out\nShe paused to decorate her interval.\n\nThere would be iridescence when she turned around\nAn ornamental lapse would recite\nthe story of her placid travels\n\nAs she swam in tranquil lapse, an uneasiness took over\n\nShe had come too far without an emotion\nAll she desired, was a frantic endeavour";
				objective = "All she desired, was a frantic endeavour";
			}
			break;
		case PlayerState.Disturbed:
			{
				story_possibilities[0] = "The greed of fulfillment\nbowed to celebration\nAs their souls echoed\nwith each other\n\nWith time\nFrom nowhere,\nA sharp discord ran through her body\nAnd just like that,\nThe resonance was over.\n\nHer final step was to find her first self";
				story_possibilities [1] = "The greed of fulfillment\nbowed to celebration\nAs their souls echoed\nwith each other\n\nWith time\nFrom nowhere,\nA sharp discord ran through her body\nAnd just like that,\nThe resonance was over.\n\nHer final step was to find her first self";
				objective = "Her final step was to find her first self";
			}
			break;
		case PlayerState.AnEnd:
			{
				story_possibilities [0] = "There is an immeasurable amount of song and dance\nWithin us\n\nWithin us,\nAre also tendrils of gleam\nParticipating in the mass celebrations\nTwirling\nRhythmically\nWith ornate hearts\nAnd limpid breath\nLet’s voyage\nIn.\n\nGAME END.\nJust Swim.";
				story_possibilities [1] = "There is an immeasurable amount of song and dance\nWithin us\n\nWithin us,\nAre also tendrils of gleam\nParticipating in the mass celebrations\nTwirling\nRhythmically\nWith ornate hearts\nAnd limpid breath\nLet’s voyage\nIn.\n\nGAME END.\nJust Swim.";
			}
			break;
		case PlayerState.LonelyEnd:
			{
				story_possibilities[0] = "You misunderstood her\n\nGAME OVER";
				story_possibilities [1] = "You misunderstood her\n\nGAME OVER";
			}
			break;
		default:
			displayStory = false;
			break;
		}

		if (displayStory) 
		{
			story = story_possibilities [chooseStory ()];
			canvasStory.SetActive (true);
			GameObject.FindGameObjectWithTag ("StoryText").GetComponent<Text> ().text = story;
		}

		if (objective != string.Empty) {
			canvasObjective.SetActive (true);
			GameObject.FindGameObjectWithTag ("ObjectiveText").GetComponent<Text> ().text = objective;
		} else {
			canvasObjective.SetActive (false);
		}

		UpdateEnvironment (playerState);
	}

	private int chooseStory(){
		return Random.Range (0, 2);
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
		GameObject.Find("Hero").GetComponent<SpriteRenderer>().color = particleColor;

		//GameObject.Find("Soundtrack").GetComponent<AudioClip>();
	}
}
