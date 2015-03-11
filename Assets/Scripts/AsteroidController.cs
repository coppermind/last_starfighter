using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	[SerializeField]
	GameObject asteroidPrefab;

	[SerializeField]
	private float spawnRate;
	
	[SerializeField]
	private float spawnY;
	
	[SerializeField]
	private float minX;
	
	[SerializeField]
	private float maxX;
	
	void Update () {
		float probability = spawnRate * Time.deltaTime;
		if (Random.value < probability) {
			Debug.Log("Spawning asteroid...");
			Vector3 position = GetRandomPosition();
			Instantiate(asteroidPrefab, position, Quaternion.identity);
		}
	}
	
	Vector3 GetRandomPosition() {
		float x = Random.Range(minX, maxX);
		return new Vector3(x, spawnY, 0f);
	}
}
