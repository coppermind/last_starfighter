using UnityEngine;
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
			float probability = exitRate * Time.deltaTime;
			if (Random.value < probability) {
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
			Destroy(gameObject);
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
}
