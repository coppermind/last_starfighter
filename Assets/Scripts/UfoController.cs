using UnityEngine;
using System.Collections;

public class UfoController : MonoBehaviour {

	[SerializeField]
	private float leftSpawnX = -5f;
	
	[SerializeField]
	private float rightSpawnX = 37f;

	[SerializeField]
	private float spawnRate = 0.1f;

	[SerializeField]
	private GameObject[] ufoPrefabs;
	
	void Update () {
		if (GameMath.IsProbable(spawnRate)) {
			SpawnUfo();
		}
	}
	
	void SpawnUfo() {
		//
	}
}
