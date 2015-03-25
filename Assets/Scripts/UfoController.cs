using UnityEngine;
using System.Collections;

public class UfoController : MonoBehaviour {

	[SerializeField]
	private float leftSpawnX = -5f;
	
	[SerializeField]
	private float rightSpawnX = 37f;
	
	[SerializeField]
	private float maxY = 17f;
	
	[SerializeField]
	private float minY = 7f;

	public float spawnRate = 0.1f;

	public GameObject[] ufoPrefabs;
	
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
