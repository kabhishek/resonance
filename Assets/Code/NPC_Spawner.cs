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
		Vector2 spawnPositionInViewPort = new Vector2 (1, Random.Range (0.0f, 1.0f));
	  	Vector2 spawnPositionInWorld = Camera.main.ViewportToWorldPoint (spawnPositionInViewPort);
	  	GameObject local = GameObject.Instantiate<GameObject> (npcPrefab, spawnPositionInWorld, Quaternion.identity);
		NPC npc = local.GetComponent<NPC> ();
		int[] omegas = new int[] { 1, 3, -1, -3, 0};
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
