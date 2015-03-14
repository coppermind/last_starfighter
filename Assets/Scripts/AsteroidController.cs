using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	#region Transform Members
	[SerializeField]
	private float spawnY;
	
	[SerializeField]
	private float minX;
	
	[SerializeField]
	private float maxX;
	#endregion
		
	
	#region Gameplay Members
	[SerializeField]
	GameObject[] asteroidPrefabs;

	[SerializeField]
	private float spawnRate;
	#endregion
	
	
	#region Unity Methods	
	void Update () {
		if (GameMath.IsProbable(spawnRate)) {
			Vector3 position = GetRandomPosition();
			Instantiate(GetRandomAsteroid(), position, Quaternion.identity);
		}
	}
	#endregion
	
	
	#region Private Methods
	GameObject GetRandomAsteroid() {
		int i = Random.Range(0, asteroidPrefabs.Length);
		return asteroidPrefabs[i];
	}
	
	Vector3 GetRandomPosition() {
		float x = Random.Range(minX, maxX);
		return new Vector3(x, spawnY, 0f);
	}
	#endregion
}
