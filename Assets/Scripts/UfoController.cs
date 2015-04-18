using UnityEngine;
using System.Collections;

public class UfoController : MonoBehaviour {

	public float spawnRate = 0.1f;
	
	public GameObject[] ufoPrefabs;
	
	public float maxY = 17f;
	
	public float minY = 7f;

	float leftSpawnX = -5f;
	float rightSpawnX = 37f;
	
	GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	void Update () {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning) {
			return;
		}
		
		if (GameMath.IsProbable(CurrentSpawnRate()) && NoUfoInPlay()) {
			SpawnUfo();
		}
	}
	
	bool NoUfoInPlay() {
		return !GameObjects.HasChildren("UFO Container");
	}
	
	void SpawnUfo() {
		int index = Random.Range(0, ufoPrefabs.Length-1);
		Vector3 position = GetRandomPosition();
		Instantiate(ufoPrefabs[index], position, Quaternion.identity);
	}
	
	float CurrentSpawnRate() {
		return spawnRate * DifficultyModifier.ForUfoSpawnRate();
	}
	
	Vector3 GetRandomPosition() {
		float[] xRange = new float[] {leftSpawnX, rightSpawnX};
		int i = Random.Range(0, 2);
		float y = Random.Range(minY, maxY);
		float x = xRange[i];
		
		return new Vector3(x, y, 0f);
	}
}
