using UnityEngine;
using System.Collections;

public class BackgroundFight : MonoBehaviour {

	public float spawnRate = 0.5f;

	public float minX = 0f;
	public float maxX = 30f;
	public float minY = 0f;
	public float maxY = 20f;
	
	[SerializeField]
	GameObject backgroundPrefab;
	

	void Update () {
		if (GameMath.IsProbable(spawnRate)) {
			SpawnBackground();
		}
	}
	
	void SpawnBackground() {
		Instantiate(backgroundPrefab, GetRandomPosition(), Quaternion.identity);
	}
	
	Vector3 GetRandomPosition() {
		float x = Random.Range(minX, maxX);
		float y = Random.Range(minY, maxY);
		return new Vector3(x, y, 0f);
	}
}
