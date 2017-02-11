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
		float step = omega * 10.0f;
		if (waveInfo != null) {
			waveInfo.WaveTrailRenderer.curveStep += step;
               waveInfo.WaveTrailRenderer.curveStep = Mathf.Clamp (waveInfo.WaveTrailRenderer.curveStep, -100f, 100f);
               Debug.Log ("Curve Step " + step);
               Debug.Log ("WaveCurve " + waveInfo.WaveTrailRenderer.curveStep);
			Debug.Log ("omega " + omega);
		}

		if (playerState == PlayerState.Lonely) 
		{
			playerState = PlayerState.LonelyEnd;
			updateStateEvent.Invoke (playerState, previousState);
		} 
		else if (playerState == PlayerState.Exploring) 
		{
			Debug.Log ("Explore Count " + exploringCount);
               if (++exploringCount > storyData.exploreCount && Mathf.Abs(waveInfo.WaveTrailRenderer.curveStep) <= 20) {
					playerState = PlayerState.Calm;
					updateStateEvent.Invoke (playerState, previousState);
				}
		} else if (playerState == PlayerState.Calm) {
               if (Mathf.Abs (waveInfo.WaveTrailRenderer.curveStep) >= 50) {
				playerState = PlayerState.Disturbed;
				updateStateEvent.Invoke (playerState, previousState);
			}
		}
		else if (playerState == PlayerState.Disturbed) 
		{
			if (Mathf.Abs(waveInfo.WaveTrailRenderer.curveStep) == 0)
			{
				playerState = PlayerState.AnEnd;
				updateStateEvent.Invoke (playerState, previousState);
				endTimer = 0;
			}
		}
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
	}
}
