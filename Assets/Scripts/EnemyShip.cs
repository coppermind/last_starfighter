﻿using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	#region Transform Members
	public float warpInSpeed = 2f;
	
	public float warpOutSpeed = 2f;
	
	Vector3 warpTarget;
	
	float screenBottomEdge;
	#endregion
	
	
	#region Gameplay Members
	public float exitRate = 0.5f;
	
	public int scorePoints;
	
	public float hitPoints = 20f;
	float currentHitPoints;
	
	bool isLeaving = false;
	bool isSpawning = true;
	bool isTargeted = false;
	#endregion
	
	
	#region GameObject Members
	[SerializeField]
	GameObject explosionPrefab;
	
	PlayerScore playerScore;
	
	GameManager gameManager;
	#endregion


	#region Unity Methods
	void Start() {
		gameManager    = FindObjectOfType<GameManager>();
		playerScore    = FindObjectOfType<PlayerScore>();
		
		isSpawning = true;
		currentHitPoints = hitPoints;
		
		warpTarget = new Vector3(transform.position.x, -10, transform.position.z);
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
	}
	
	void Update() {
		if (gameManager.GameIsPaused) {
			return;
		}
		
		if (isSpawning) {
			WarpIn();
			
			if (transform.position == transform.parent.position) {
				isSpawning = false;
			}
		} else {
			
			if (GameMath.IsProbable(exitRate)) {
				isLeaving = true;
			}
			
			if (isLeaving) { 
				WarpOut();
			}
			
			if (transform.position.y <= screenBottomEdge) {
				Suicide();
			}
		}
	}
	#endregion
	
	
	#region Private Methods
	void Die() {
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.parent = transform;
		
		Transform body = transform.Find("Body");
		if (body) {
			Destroy(body.gameObject);
		}
		
		Destroy(gameObject, 0.5f);
		
		playerScore.AddScore(scorePoints);
	}
	
	void Suicide() {
		Destroy(gameObject);
	}
	
	void WarpIn() {
		float step = warpInSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, step);
	}
	
	void WarpOut() {
		float step = warpOutSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, warpTarget, step);
	}
	#endregion
	
	
	public bool IsTargeted {
		get { return isTargeted; }
		set { isTargeted = value; }
	}
	
	public void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			Die();
		}
	}
}
