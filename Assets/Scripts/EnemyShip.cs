using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	public float hitPoints = 20f;
	
	private float currentHitPoints;

	void Start () {
		currentHitPoints = hitPoints;
		
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerLaser laser = collider.GetComponent<PlayerLaser>();
		
		if (laser) {
			HitWith(laser.damagePoints);
		}
	}
	
	void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			Destroy(gameObject);
		}
	}
}
