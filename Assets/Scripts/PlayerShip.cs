using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {

	public float shipSpeed = 8f;
	public float shipPadding = 0.5f;
	
	public float hitPoints = 100f;
	public int numberOfLives = 3;

	public AudioClip defaultEffects;
	
	private float minX, maxX, minY, maxY;
	private Animator animator;
	
	private PlayerShield shield;
	
	private float currentHitPoints;
	
	private LevelManager levelManager;
	
	void Start() {
		CalculateCameraDistance();
		
		levelManager = FindObjectOfType<LevelManager>();
		shield  = GetComponentInChildren<PlayerShield>();
		
		currentHitPoints = hitPoints;
	}

	void CalculateCameraDistance() {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		minX = camera.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + shipPadding;
		minY = camera.ViewportToWorldPoint (new Vector3 (0, 0, distance)).y + shipPadding;
		maxX = camera.ViewportToWorldPoint (new Vector3 (1, 1, distance)).x - shipPadding;
		maxY = camera.ViewportToWorldPoint (new Vector3 (1, 1, distance)).y - shipPadding;
	}
	
	void Update() {
		Vector3 shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		float newXPosition = shipPosition.x;
		float newYPosition = shipPosition.y;
		
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			newXPosition = transform.position.x - shipSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			newXPosition = transform.position.x + shipSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			newYPosition = transform.position.y + shipSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			newYPosition = transform.position.y - shipSpeed * Time.deltaTime;
		}
		UpdateShipPosition(shipPosition, newXPosition, newYPosition);
	}
	
	void UpdateShipPosition(Vector3 ship, float x, float y) {
		ship.x = Mathf.Clamp(x, minX, maxX);
		ship.y = Mathf.Clamp(y, minY, maxY);
		transform.position = ship;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyLaser enemyLaser = collider.GetComponent<EnemyLaser>();
		
		if (enemyLaser) {
			if (!shield.shieldIsDown()) {
				shield.HitWith(enemyLaser.damagePoints);
			} else {
				HitWith(enemyLaser.damagePoints);
			}
		}
	}
	
	void HitWith(float damage) {
		Debug.Log("Ship shield is down, player hit with " + damage + " damage.");
		currentHitPoints -= damage;
		if (0f <= currentHitPoints) {
			Destroy(gameObject);
			levelManager.LoadLevel("04 Lose Screen");
		}
	}
}
