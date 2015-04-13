using UnityEngine;
using System.Collections;

public class BomberShip : MonoBehaviour {

	[SerializeField]
	int scorePoints;
	
	[SerializeField]
	float hitPoints = 20f;
	float currentHitPoints;
	
	public float minX = -5f;
	
	public float maxX = 37f;
	
	[SerializeField]
	float shipSpeed = 4f;
	
	float shipDirection = -1;
	
	[SerializeField]
	GameObject explosionPrefab;
	
	[SerializeField]
	AudioClip hitClip;
	
	[SerializeField]
	AudioClip explosionClip;
	
	Rigidbody2D rigidBody;
	
	PlayerScore playerScore;
	
	AudioSource audioSource;
	
	GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		playerScore = FindObjectOfType<PlayerScore>();
		
		rigidBody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		
		currentHitPoints = hitPoints * DifficultyModifier.ForBomberHitPoints();
		
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
	
	void Die() {
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.parent = transform;
		
		Transform body = transform.Find("Body");
		if (body) {
			Destroy(body.gameObject);
		}
		
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		collider.enabled = false;
		Destroy(gameObject, 0.5f);
		
		playerScore.AddScore(scorePoints);
	}
	
	float CurrentShipSpeed() {
		return shipSpeed * DifficultyModifier.ForBomberSpeed();
	}
	
	Vector3 GetVelocity() {
		float speed = CurrentShipSpeed() * shipDirection;
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
	
	public void HitWith(float damage) {
		currentHitPoints -= damage;
		
		if (0f >= currentHitPoints) {
			audioSource.clip = explosionClip;
			Die();
		} else {
			audioSource.clip = hitClip;
		}
		
		audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
		audioSource.Play();
	}
	
	public void HitWith() {
		HitWith(currentHitPoints);
	}
}
