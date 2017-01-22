using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour {

	[SerializeField] private MonoBehaviour collisionInfo;
	[SerializeField] private GameObject canvasStory;

	private ICollisionInfo iCollisionInfo; 
	private Color bgColor;

	// Use this for initialization
	void Start () {
		iCollisionInfo = collisionInfo.GetComponent<ICollisionInfo> ();
		iCollisionInfo.PlayerCollision.AddListener (UpdateStory);

		bgColor = new Color ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateStory(float state)
	{
		UpdateEnvironment (state);
	
		canvasStory.SetActive(true);
		GameObject.FindGameObjectWithTag ("StoryText").GetComponent<Text> ().text = "These are cautiously pleated waves \n of us, of you.\n Shall we? \nas we resonate.";
	}

	void UpdateEnvironment(float state){
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
