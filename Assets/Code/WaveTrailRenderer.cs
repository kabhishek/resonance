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
	// Use this for initialization
	void Start () 
	{
//		iWaveInfo = waveInfo.GetComponent<IWaveInfo>();
//		lineRenderer.SetPositions();
//		float thetaStep = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		iWaveInfo.WavePoints;		
//		lineRenderer.SetPositions(new [] { new Vector3(0,0), new Vector3(1,1), new Vector3(2,2) });
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
			//float y = 0;
			curvePositions[i] = new Vector3(x, y) + transform.position;
			//			lineRenderer.SetPosition (i, curvePositions [i]);
			x -= Time.deltaTime * factor;
			//curveStep += curveStep;
		}
		lineRenderer.SetPositions (curvePositions);

	}
}
