using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;

	[SerializeField]
	private float defaultProjectileSpeed = -10f;
	float currentProjectileSpeed;

	float screenBottomEdge;
	
	Rigidbody2D rigidBody;
	
	GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		rigidBody.velocity = new Vector3(0f, CurrentProjectileSpeed(), 0f);
		
		GameObjects.SetParent(transform, "Enemy Laser Container");
	}
	
	void Update () {
		if (gameManager.GameIsPaused) {
			rigidBody.velocity = new Vector3(0f, 0f, 0f);
			return;
		}
		
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge <= screenBottomEdge) {
			Destroy(gameObject);
		}
		
		rigidBody.velocity = new Vector3(0f, CurrentProjectileSpeed(), 0f);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
		
		if (player) {
			Destroy(gameObject);
		}
	}
	
	float CurrentProjectileSpeed() {
		return currentProjectileSpeed * DifficultyModifier.ForEnemyLaserSpeed();
	}
	
	public float DamagePoints {
		get { return damagePoints * DifficultyModifier.ForEnemyLaserDamage(); }
		set { damagePoints = value; }
	}
}
