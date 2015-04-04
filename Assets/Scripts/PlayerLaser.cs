using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	[SerializeField]
	float damagePoints = 20f;
	
	[SerializeField]
	float defaultProjectileSpeed = 10f;
	float currentProjectileSpeed;

	float screenTopEdge;
	
	Rigidbody2D rigidBody;
	
	GameManager gameManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
	
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		rigidBody.velocity = new Vector3(0f, CurrentProjectileSpeed(), 0f);
		
		GameObjects.SetParent(transform, "Player Laser Container");
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
		
		rigidBody.velocity = new Vector3(0f, CurrentProjectileSpeed(), 0f);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		BomberShip bomber = collider.gameObject.GetComponent<BomberShip>();
		Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
		
		if (enemy || bomber) {
			Destroy(gameObject);
			if (enemy)  { enemy.HitWith(damagePoints); }
			if (bomber) { bomber.HitWith(damagePoints); }
		}
		
		if (asteroid) {
			Destroy(gameObject);
			asteroid.Destroy();
		}
	}
	
	float CurrentProjectileSpeed() {
		return currentProjectileSpeed * DifficultyModifier.ForEnemyLaserSpeed();
	}
	
	public float DamagePoints {
		get { return damagePoints * DifficultyModifier.ForPlayerLaserDamage(); }
		set { damagePoints = value; }
	}
}
