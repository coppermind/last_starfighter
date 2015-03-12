using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	[SerializeField]
	GameObject[] asteroidPrefabs;

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
			Vector3 position = GetRandomPosition();
			Instantiate(GetRandomAsteroid(), position, Quaternion.identity);
		}
	}
	
	GameObject GetRandomAsteroid() {
		int i = Random.Range(0, asteroidPrefabs.Length);
		return asteroidPrefabs[i];
	}
	
	Vector3 GetRandomPosition() {
		float x = Random.Range(minX, maxX);
		return new Vector3(x, spawnY, 0f);
	}
}
