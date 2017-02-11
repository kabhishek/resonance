using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour {

	[SerializeField] private GameObject npcPrefab;

	private List<GameObject> spawnedNPCs = new List<GameObject>();
	private float startTimer;
	private float randomStartTime;
	[SerializeField] private float spawnTimeDelayMin = 40f;
	[SerializeField] private float spawnTimeDelayMax = 70f;

	private void Start()
	{
		startTimer = Time.time;
		randomStartTime = Random.Range (startTimer + spawnTimeDelayMin, startTimer + spawnTimeDelayMax);
	}

	public void Spawn()
	{
		Vector2 spawnPositionInViewPort = new Vector2 (1.1f, Random.Range (0.25f, 0.75f));
//		Debug.Log ("Spawn Viewport Pos " + spawnPositionInViewPort);
	  	Vector2 spawnPositionInWorld = Camera.main.ViewportToWorldPoint (spawnPositionInViewPort);
//		Debug.Log ("Spawn World Pos" + spawnPositionInWorld);
	  	GameObject local = GameObject.Instantiate<GameObject> (npcPrefab, spawnPositionInWorld, Quaternion.identity);
		NPC npc = local.GetComponent<NPC> ();
		int[] omegas = new int[] { 0, 1, -1, 1, -1, 2, -2, 2, -2, 3, -3, 3, -3, 5, -5, 0};
//		int[] omegas = new int[] { 0, 0, 0, 0, 0};
		//float[] omegas = new float[] { 100f };
		npc.omega = omegas[Random.Range(0, omegas.Length - 1)];
		TrailRenderer tr = local.GetComponent<TrailRenderer> ();
		tr.sortingLayerName = "Gameplay";
	  	spawnedNPCs.Add (local);
	}

	private void Update()
	{
		startTimer += Time.deltaTime;
		if (startTimer > randomStartTime)
		{
			Spawn();
			startTimer = Time.time;
			randomStartTime = Random.Range (startTimer + spawnTimeDelayMin, startTimer + spawnTimeDelayMax);
		}
	}
}
