using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour {

     [SerializeField] private GameObject npcPrefab;

     private List<GameObject> spawnedNPCs = new List<GameObject>();

     // Use this for initialization
	private void Start () {
	
	}
	
	// Update is called once per frame
     private void Update () {
	}

     public void Spawn()
     {
          Vector2 spawnPositionInViewPort = new Vector2 (1, Random.Range (0.0f, 1.0f));
          Vector2 spawnPositionInWorld = Camera.main.ViewportToWorldPoint (spawnPositionInViewPort);
          GameObject local = GameObject.Instantiate<GameObject> (npcPrefab, spawnPositionInWorld, Quaternion.identity);
          spawnedNPCs.Add (local);
     }
}
