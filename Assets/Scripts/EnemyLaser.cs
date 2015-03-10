﻿using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	public float damagePoints = 20f;
	
	private float screenTopEdge;
	
	void Start () {
		Camera camera  = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		screenTopEdge  = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge <= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
		if (player) {
			Debug.Log("Hit player ship!");
			Destroy(gameObject);
		}
	}
}