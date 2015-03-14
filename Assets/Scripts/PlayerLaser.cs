using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	[SerializeField]
	private float defaultProjectileSpeed = 10f;
	private float currentProjectileSpeed;

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenTopEdge;
	
	private Rigidbody2D rigidBody;
	
	private GameManager gameManager;

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
		
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge >= screenTopEdge) {
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
