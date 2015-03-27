using UnityEngine;
using System.Collections;

public class UfoController : MonoBehaviour {

	public float spawnRate = 0.1f;
	
	public GameObject[] ufoPrefabs;

	[SerializeField]
	float leftSpawnX = -5f;
	
	[SerializeField]
	float rightSpawnX = 37f;
	
	[SerializeField]
	float maxY = 17f;
	
	[SerializeField]
	float minY = 7f;
	
	void Update () {
		if (GameMath.IsProbable(spawnRate)) {
			SpawnUfo();
		}
	}
	
	void SpawnUfo() {
		int index = Random.Range(0, ufoPrefabs.Length-1);
		Vector3 position = GetRandomPosition();
		Instantiate(ufoPrefabs[index], position, Quaternion.identity);
	}
	
	Vector3 GetRandomPosition() {
		float[] xRange = new float[] {leftSpawnX, rightSpawnX};
		int i = Random.Range(0, 2);
		float y = Random.Range(minY, maxY);
		float x = xRange[i];
		
		return new Vector3(x, y, 0f);
	}
}
