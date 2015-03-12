using UnityEngine;
using System.Collections;

public class BackgroundFight : MonoBehaviour {

	[SerializeField]
	private GameObject backgroundPrefab;

	[SerializeField]
	private float spawnRate = 0.5f;
	
	[SerializeField]
	private float minX = 0f;
	
	[SerializeField]
	private float maxX = 30f;
	
	[SerializeField]
	private float minY = 0f;
	
	[SerializeField]
	private float maxY = 20f;

	void Update () {
		float probability = spawnRate * Time.deltaTime;
		if (Random.value < probability) {
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
