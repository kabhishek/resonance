using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrailRenderer : MonoBehaviour {

	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private MonoBehaviour waveInfo;

	private IWaveInfo iWaveInfo;
	private Vector3[] curvePositions = new Vector3[100];
	public float curveStep;


	private void Awake()
	{
		lineRenderer.sortingLayerName = "Gameplay";
	}
		
	void Update () 
	{
		UpdatePositions();
	}

	private void UpdatePositions()
	{
		float amplitudeY = 1.0f;
		float x = 0;
		float factor = 10;
		float index = 1;

		lineRenderer.numPositions = 99;
		for (int i = 0; i < 50; i++)
		{
			float y = amplitudeY * Mathf.Sin (curveStep * index);
			index += Time.fixedDeltaTime;
			curvePositions[i] = new Vector3(x, y) + transform.position;
			x -= Time.deltaTime * factor;
		}
		lineRenderer.SetPositions (curvePositions);

	}
}
