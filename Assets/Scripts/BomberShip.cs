using UnityEngine;
using System.Collections;

public class BomberShip : MonoBehaviour {

	[SerializeField]
	int scorePoints;
	
	[SerializeField]
	float hitPoints = 20f;
	float currentHitPoints;
	
	public int barrageProjectileCount = 3;
	
	public float minX = -5f;
	
	public float maxX = 37f;
	
	[SerializeField]
	float shipSpeed = 4f;
	
	float shipDirection = -1;
	
	[SerializeField]
	float projectileRate = 1f;
	bool isShooting = false;
	
	[SerializeField]
	GameObject explosionPrefab;
	
	Rigidbody2D rigidBody;
	
	PlayerScore playerScore;
	
	GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		playerScore = FindObjectOfType<PlayerScore>();
		
		rigidBody = GetComponent<Rigidbody2D>();
		
		currentHitPoints = hitPoints;
		
		GameObjects.SetParent(transform, "Bomber Controller");
		SetDirection();
	}
	
	void Update () {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning) {
			rigidBody.velocity = new Vector3(0f, 0f, 0f);
			return;
		}
		
		if (minX <= transform.position.x && transform.position.x <= maxX) {
			rigidBody.velocity = GetVelocity();
		} else {
			Destroy(gameObject);
		}
	}
	
	public void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			Die();
		}
	}
	
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
	
	Vector3 GetVelocity() {
		float speed = shipSpeed * shipDirection;
		return new Vector3(speed , 0f, 0f);
	}
	
	void SetDirection() {
		Transform bodyTransform = transform.FindChild("Body");
		
		if (0 < transform.position.x) {
			shipDirection = -1;
		} else {
			shipDirection = 1;
			bodyTransform.Rotate(new Vector3(0f, 0f, -180f));
		}
	}
}
