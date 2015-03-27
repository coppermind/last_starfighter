using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	public float defaultProjectileSpeed = 10f;
	float currentProjectileSpeed;

	public float damagePoints = 20f;
	
	float screenTopEdge;
	
	Rigidbody2D rigidBody;
	
	GameManager gameManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
	
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		rigidBody.velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void Update () {
		if (gameManager.GameIsPaused) {
			rigidBody.velocity = new Vector3(0f, 0f, 0f);
			return;
		}
		
		float laserEdge = transform.position.y;
		if (laserEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
		
		rigidBody.velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
		
		if (enemy) {
			Destroy(gameObject);
			enemy.HitWith(damagePoints);
		} 
		
		if (asteroid) {
			Destroy(gameObject);
			asteroid.Destroy();
		}
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
