using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	#region Transform Members
	public float speed = 1.5f;
	
	public float width = 15f;
	
	public float height = 10f;
	
	public float padding = 0.5f;
	
	public float spawnY;
	
	int direction = 1;
	
	float screenRightEdge, screenLeftEdge;
	#endregion
	
	
	#region Gameplay Members
	public float spawnRate = 0.5f;
	#endregion


	#region GameObject Members
	[SerializeField]
	GameObject enemyPrefab;
	
	GameManager gameManager;
	#endregion
	
	
	#region Unity Methods
	void Start () {
		gameManager  = FindObjectOfType<GameManager>();
		
		GetCameraPosition();
	}
	
	void Update () {
		if (gameManager.GameIsPaused) {
			return;
		}
			
		float formationRightEdge = transform.position.x + padding * width;
		float formationLeftEdge  = transform.position.x - padding * width;
		
		if (formationLeftEdge <= screenLeftEdge) {
			direction = 1;
		} else if (formationRightEdge >= screenRightEdge) {
			direction = -1;
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
		
		if (!gameManager.PlayerIsSpawning) {
			SpawnEnemyShip();
		}
	}
	
	void OnDrawGizmos() {
		GameGizmos.DrawBox(transform, width, height, padding);
	}
	#endregion
	
	
	#region Private Methods
	void GetCameraPosition() {
		Hashtable b = GameCamera.GetBoundaries(Camera.main, transform, padding);
		
		screenLeftEdge  = (float) b["minX"];
		screenRightEdge = (float) b["maxX"];
	}
	
	void SpawnEnemyShip() {
		if (GameMath.IsProbable(CurrentSpawnRate())) {
			Transform freePosition = NextFreePosition();
			if (null != freePosition) {
				SpawnEnemyShipAt(freePosition, freePosition.position);
			}
		}
	}
	
	float CurrentSpawnRate() {
		return spawnRate * DifficultyModifier.ForEnemySpawnRate();
	}
	
	Transform NextFreePosition() {
		EnemyPosition[] spawnPoints = GetComponentsInChildren<EnemyPosition>();
		int points = spawnPoints.Length + 1;
		
		for (int i = 0; i <= points; i++) {
			int current = Random.Range(0, points-1);
			if (0 == spawnPoints[current].transform.childCount) {
				return spawnPoints[current].transform;
			}
		}
		return null;
	}
	
	void SpawnEnemyShipAt(Transform parentElement, Vector3 shipPosition) {
		GameObject enemy         = Instantiate(enemyPrefab, shipPosition, Quaternion.identity) as GameObject;
		enemy.transform.parent   = parentElement;
		enemy.transform.position = new Vector3(shipPosition.x, spawnY, shipPosition.z);
	}
	
	bool AllShipsAreDead() {
		foreach (Transform position in transform) {
			if (0 < position.childCount) {
				return false;
			}
		}
		return true;
	}
	#endregion
	
}
