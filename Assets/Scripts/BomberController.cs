﻿using UnityEngine;
using System.Collections;

public class BomberController : MonoBehaviour {

	public float spawnRate = 0.1f;
	
	public GameObject bomberPrefab;
	
	public float maxY = 16f;
	
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
		
		if (GameMath.IsProbable(CurrentSpawnRate()) && 0 >= transform.childCount) {
			SpawnBomber();
		}
	}
	
	void SpawnBomber() {
		Vector3 position = GetRandomPosition();
		Instantiate(bomberPrefab, position, Quaternion.identity);
	}
	
	float CurrentSpawnRate() {
		return spawnRate * DifficultyModifier.ForBomberSpawnRate();
	}
	
	Vector3 GetRandomPosition() {
		float[] xRange = new float[] {leftSpawnX, rightSpawnX};
		int i = Random.Range(0, 2);
		float y = Random.Range(minY, maxY);
		float x = xRange[i];
		
		return new Vector3(x, y, 0f);
	}
}
