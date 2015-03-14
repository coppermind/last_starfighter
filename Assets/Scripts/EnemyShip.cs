﻿using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	#region Transform Members
	[SerializeField]
	private float warpInSpeed = 2f;
	
	[SerializeField]
	private float warpOutSpeed = 2f;
	
	private Vector3 warpTarget;
	
	private float screenBottomEdge;
	#endregion
	
	
	#region Gameplay Members
	[SerializeField]
	private float score;
	
	[SerializeField]
	private float hitPoints = 20f;
	private float currentHitPoints;
	
	[SerializeField]
	private float exitRate = 0.5f;
	
	private bool isLeaving = false;
	private bool isSpawning = true;
	#endregion
	
	
	#region GameObject Members
	[SerializeField]
	private GameObject explosionPrefab;
	
	private EnemyFormation enemyFormation;
	#endregion


	#region Unity Methods
	void Start() {
		isSpawning = true;
		currentHitPoints = hitPoints;
		
		enemyFormation = FindObjectOfType<EnemyFormation>();
		
		warpTarget = new Vector3(transform.position.x, -10, transform.position.z);
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
	}
	
	void Update() {
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
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerLaser laser = collider.GetComponent<PlayerLaser>();
		
		if (laser) {
			HitWith(laser.DamagePoints);
		}
	}
	#endregion
	
	
	#region Private Methods
	void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			Die();
		}
	}
	
	void Suicide() {
		Destroy(gameObject);
		enemyFormation.KillEnemy();
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
	
	public void Die() {
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.parent = transform;
		
		GameObject body = transform.FindChild("Body").gameObject;
		Destroy(body);
		
//		Destroy(explosion, 0.5f);
		Destroy(gameObject, 0.5f);
	}
}
