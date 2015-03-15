﻿using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

	[SerializeField]
	private GameObject weaponPrefab;
	
	[SerializeField]
	private float defaultProjectileRate = 0.2f;
	private float currentProjectileRate;
	
	private GameManager gameManager;
	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		currentProjectileRate  = defaultProjectileRate;
	}
	
	void Update() {
		if (gameManager.GameIsPaused || gameManager.PlayerHasWon) {
			CancelInvoke("FirePrimaryWeapon");
			return;
		}
		
		if (!gameManager.PlayerIsSpawning && !gameManager.PlayerHasWon) {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
				StartFiringPrimaryWeapon();
			}
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) {
				StopFiringPrimaryWeapon();
			}
			if (Input.GetMouseButton(1)) {
				Debug.Log("Mouse button 1 pressed.");
			}
		}
	}
	
	void FirePrimaryWeapon() {
		Instantiate(weaponPrefab, transform.position, Quaternion.identity);
	}
	
	void FireSecondaryWeapon() {
		
	}
	
	public void StartFiringPrimaryWeapon() {
		InvokeRepeating("FirePrimaryWeapon", 0.0001f, currentProjectileRate);
	}
	
	public void StopFiringPrimaryWeapon() {
		CancelInvoke("FirePrimaryWeapon");
	}
}