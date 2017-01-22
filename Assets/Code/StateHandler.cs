using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerState
{
	Beginnings,
	Lonely,
	LonelyEnd,
	Disturbed,
	Calm,
	AnEnd,
	Exploring
}
// Current state, previous state, curve step
public class UpdateStateEvent : UnityEvent<PlayerState, PlayerState>
{
	
}

public class StateHandler : MonoBehaviour 
{
	public UpdateStateEvent updateStateEvent = new UpdateStateEvent();
	[SerializeField] private MonoBehaviour collisionInfo;
	[SerializeField] private StoryData storyData;
	private PlayerState playerState;
	private PlayerState previousState;
	private ICollisionInfo iCollisionInfo;

	private float lonelyStatetimer;
	private int exploringCount;
	private float endTimer;

	private void Awake () 
	{
		iCollisionInfo = collisionInfo.GetComponent<ICollisionInfo> ();
		iCollisionInfo.PlayerCollision.AddListener (UpdateState);
		playerState = previousState = PlayerState.Beginnings;
	}

	private void OnDestroy()
	{
		iCollisionInfo.PlayerCollision.RemoveListener (UpdateState);
	}

	private void UpdateState(float omega, IWaveInfo waveInfo)
	{
		Debug.Log ("UpdateState");
		float curveStep = omega * -10.0f;
//		if (playerState != PlayerState.Beginnings && previousState == playerState)
//			return;
		// state handling

		if (playerState == PlayerState.Lonely) 
		{
			playerState = PlayerState.LonelyEnd;
			updateStateEvent.Invoke (playerState, previousState);
		} 
		else if (playerState == PlayerState.Exploring) 
		{
			if (waveInfo != null) {
				waveInfo.WaveTrailRenderer.curveStep = curveStep;
				Debug.Log ("Curve Step " + waveInfo.WaveTrailRenderer.curveStep);
				Debug.Log ("omega " + omega);
				Debug.Log ("Explore Count " + exploringCount);
				if (++exploringCount > storyData.exploreCount) {
					playerState = PlayerState.Calm;
					updateStateEvent.Invoke (playerState, previousState);
				}
			}
		} else if (playerState == PlayerState.Calm) {
			if (Mathf.Abs (omega) >= 3) {
				playerState = PlayerState.Disturbed;
				updateStateEvent.Invoke (playerState, previousState);
			}
		}
		else if (playerState == PlayerState.Disturbed) 
		{
			if (waveInfo.WaveTrailRenderer.curveStep <= 20)
			{
				playerState = PlayerState.AnEnd;
				updateStateEvent.Invoke (playerState, previousState);
				endTimer = 0;
			}
		}

		// Post state handling
//		previousState = playerState;
//		switch (playerState) 
//		{
//			case PlayerState.Beginnings:
//			{
//				playerState = PlayerState.Lonely;
//				lonelyStatetimer = 0;
//			}
//			break;
//		}
	}

	private void Update()
	{
		if (playerState == PlayerState.Beginnings)
		{
			updateStateEvent.Invoke (playerState, previousState);
			playerState = PlayerState.Lonely;
			lonelyStatetimer = 0;
		}
		else if (playerState == PlayerState.Lonely) 
		{
			lonelyStatetimer += Time.deltaTime;
			if (lonelyStatetimer > storyData.lonelytime) 
			{
				playerState = PlayerState.Exploring;
				exploringCount = 0;
				updateStateEvent.Invoke (playerState, previousState);
			}
		}
//		if (playerState == PlayerState.AnEnd) {
//			playerState = PlayerState.Beginnings;
//			updateStateEvent.Invoke (playerState, previousState);
//		}
	}
}
