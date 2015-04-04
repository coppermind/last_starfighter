using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	#region Transform Members
	public float spawnY;
	
	public float minX;
	
	[SerializeField]
	float maxX;
	#endregion
		
	
	#region Gameplay Members
	public GameObject[] asteroidPrefabs;

	public float spawnRate;
	
	GameManager gameManager;
	#endregion
	
	
	#region Unity Methods	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	void Update() {
		if (!gameManager.PlayerIsSpawning && !gameManager.PlayerHasWon && !gameManager.GameIsPaused) {
			if (GameMath.IsProbable(CurrentSpawnRate())) {
				Vector3 position = GetRandomPosition();
				GameObject asteroid = Instantiate(GetRandomAsteroid(), position, Quaternion.identity) as GameObject;
				asteroid.transform.parent = transform;
			}
		}
	}
	#endregion
	
	
	#region Private Methods
	GameObject GetRandomAsteroid() {
		int i = Random.Range(0, asteroidPrefabs.Length);
		return asteroidPrefabs[i];
	}
	
	float CurrentSpawnRate() {
		return spawnRate * DifficultyModifier.ForAsteroidSpawnRate();
	}
	
	Vector3 GetRandomPosition() {
		float x = Random.Range(minX, maxX);
		return new Vector3(x, spawnY, 0f);
	}
	#endregion
}
