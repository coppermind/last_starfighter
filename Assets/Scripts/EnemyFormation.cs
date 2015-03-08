using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 15f;
	public float height = 10f;
	public float padding = 0.5f;
	public float spawnInterval = 0.5f;
	public int shipCount = 0;
	
	private float screenRightEdge, screenLeftEdge;

	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		
		screenLeftEdge   = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		screenRightEdge  = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
		
		SpawnUntilFull();
	}
	
	void Update () {
	
	}
	
	void OnDrawGizmos() {
		float xMin, xMax, yMin, yMax;
		
		xMin = transform.position.x - padding * width;
		xMax = transform.position.x + padding * width;
		yMin = transform.position.y - padding * height;
		yMax = transform.position.y + padding * height;
		
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMin, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMax, yMin, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMax, 0f), new Vector3(xMax, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMax, yMin, 0f), new Vector3(xMax, yMax, 0f));
	}
	
	void SpawnUntilFull() {
		//Debug.Log("Spawning until full...");
		Transform freePosition = NextFreePosition();
		if (null != freePosition) {
			SpawnEnemyShipAt(freePosition, freePosition.position);
			Invoke("SpawnUntilFull", spawnInterval);
		}
	}
	
	Transform NextFreePosition() {
		EnemyPosition[] spawnPoints = GetComponentsInChildren<EnemyPosition>();
		int points = spawnPoints.Length;
		
		for (int i = 0; i <= points; i++) {
			int current = Random.Range(0, points-1);
			if (0 == spawnPoints[current].transform.childCount) {
				return spawnPoints[current].transform;
			}

		}
		return null;
	}
	
	void SpawnEnemyShipAt(Transform parentElement, Vector3 shipPosition) {
		//Debug.Log("SpawnEnemyShipAt");
		GameObject enemy = Instantiate(enemyPrefab, shipPosition, Quaternion.identity) as GameObject;
		enemy.transform.parent = parentElement;
		enemy.transform.position = shipPosition;
		shipCount++;
	}
}
