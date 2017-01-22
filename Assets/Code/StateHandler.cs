using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerState
{
	Beginnings,
	Lonely,
	Disturbed,
	Calm,
	AnEnd,
	Exploring
}
// Current state, previous state
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
		if (playerState != PlayerState.Beginnings && previousState == playerState)
			return;
		// state handling

		if (playerState == PlayerState.Lonely) 
		{
			playerState = PlayerState.AnEnd;
		}
		else if (playerState == PlayerState.Exploring) 
		{
			if (waveInfo != null) 
			{
				waveInfo.WaveTrailRenderer.curveStep = omega * -10.0f;
				Debug.Log ("Curve Step " + waveInfo.WaveTrailRenderer.curveStep);
				Debug.Log ("omega " + omega);
			}
		}

		updateStateEvent.Invoke (playerState, previousState);

		// Post state handling
		previousState = playerState;
		switch (playerState) 
		{
			case PlayerState.Beginnings:
			{
				playerState = PlayerState.Lonely;
				lonelyStatetimer = 0;
			}
			break;
		}
	}

	private void Update()
	{
		if (playerState == PlayerState.Beginnings)
		{
			UpdateState (0, null);
		}
		if (playerState == PlayerState.Lonely) 
		{
			lonelyStatetimer += Time.deltaTime;
			if (lonelyStatetimer > storyData.lonelytime) 
			{
				playerState = PlayerState.Exploring;
			}
		}
	}
}
