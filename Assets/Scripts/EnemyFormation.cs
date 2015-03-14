﻿using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	#region Transform Members
	[SerializeField]
	private float spawnY;
	
	[SerializeField]
	private float width = 15f;
	
	[SerializeField]
	private float height = 10f;
	
	[SerializeField]
	private float padding = 0.5f;
	
	[SerializeField]
	private float speed = 1.5f;
	
	private int direction = 1;
	
	private float screenRightEdge, screenLeftEdge;
	#endregion
	
	
	#region Gameplay Members
	[SerializeField]
	private float spawnRate = 0.5f;
	
	[SerializeField]
	private int shipCount = 0;
	
	[SerializeField]
	private int enemiesInThisLevel = 10;
	private int enemiesLeft;
	#endregion


	#region GameObject Members
	[SerializeField]
	private GameObject enemyPrefab;
	
	private GameManager gameManager;
	
	private PlayerShip playerShip;
	#endregion
	
	
	#region Unity Methods
	void Start () {
		gameManager  = FindObjectOfType<GameManager>();
		playerShip   = FindObjectOfType<PlayerShip>();
		
		enemiesLeft = enemiesInThisLevel;
		
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
		if (0 < enemiesLeft) {
			if (GameMath.IsProbable(spawnRate)) {
				Transform freePosition = NextFreePosition();
				if (null != freePosition) {
					SpawnEnemyShipAt(freePosition, freePosition.position);
				}
			}
		}
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
		shipCount++;
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
	
	
	#region Public Methods
	public void KillEnemy() {
		enemiesLeft--;
		if (0 >= enemiesLeft) {
			playerShip.LoadNextLevel();
		}
	}
	#endregion
}
