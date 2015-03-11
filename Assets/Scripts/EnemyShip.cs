using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	[SerializeField]
	private float hitPoints = 20f;
	
	[SerializeField]
	private float warpSpeed = 2f;
	
	[SerializeField]
	private float exitRate = 0.5f;
	
	private float currentHitPoints;
	
	private float screenBottomEdge;
	
	private bool engageWarp = false;
	
	private Vector3 warpTarget;
	
	private EnemyFormation enemyFormation;

	void Start() {
		currentHitPoints = hitPoints;
		
		enemyFormation = FindObjectOfType<EnemyFormation>();
		
		warpTarget = new Vector3(transform.position.x, -10, transform.position.z);
		
		Camera camera     = Camera.main;
		float distance    = transform.position.z - camera.transform.position.z;
		screenBottomEdge  = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
	}
	
	void Update() {
		float probability = exitRate * Time.deltaTime;
		if (Random.value < probability) {
			engageWarp = true;
		}
		
		if (engageWarp) {
			WarpShip();
		}
		
		if (transform.position.y <= screenBottomEdge) {
			Suicide();
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerLaser laser = collider.GetComponent<PlayerLaser>();
		
		if (laser) {
			HitWith(laser.DamagePoints);
		}
	}
	
	void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			Destroy(gameObject);
		}
	}
	
	void WarpShip() {
		float step = warpSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, warpTarget, step);
	}
	
	void Suicide() {
		Destroy(gameObject);
		enemyFormation.KillEnemy();
	}
}
