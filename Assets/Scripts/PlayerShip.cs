using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
	
	#region Transform Members
	public float shipSpeed = 8f;
	
	public float minY = 2f;
	
	public float maxY = 4f;
	
	public float spawnTargetX = 16f;
	
	public float spawnTargetY = 1f;
	
	public float spawnWarpSpeed = 2.5f;
	
	public float exitWarpSpeed = 5f;
	
	public float exitTargetY = 21f;
	
	public float shipPadding = 0.5f;
	
	Vector3 spawnInTarget, exitTarget;
	
	float minX, maxX;
	#endregion	
	
	
	#region Gameplay Members
	[SerializeField]
	float hitPoints = 100f;
	float currentHitPoints;
	
	/*
	[SerializeField]
	int numberOfLives = 3;
	*/
	#endregion


	#region Component Members
	Animator animator;
	
	PlayerShield shield;
	PlayerGun gun;
	
	CircleCollider2D shipCollider;
	#endregion

		
	#region GameObject Members
	LevelManager levelManager;
	
	GameManager gameManager;
	#endregion
	
	
	#region Unity Methods
	void Start() {
		levelManager = FindObjectOfType<LevelManager>();
		gameManager  = FindObjectOfType<GameManager>();
		
		shield       = GetComponentInChildren<PlayerShield>();
		gun          = GetComponentInChildren<PlayerGun>();

		shipCollider = GetComponent<CircleCollider2D>();
		shipCollider.enabled = false;
		
		gameManager.PlayerIsSpawning = true;
		spawnInTarget = new Vector3(spawnTargetX, spawnTargetY, transform.position.z);
		
		currentHitPoints = hitPoints;
		CalculateCameraDistance();
	}

	void Update() {
		if (gameManager.GameIsPaused) {
			return;
		}
		
		if (gameManager.PlayerIsSpawning) {
			WarpIn();
			
			if (transform.position == spawnInTarget) {
				gameManager.PlayerIsSpawning = false;
				shipCollider.enabled = true;
			}
			
		} else if (gameManager.PlayerHasWon) {
			FtlJump();
		
		} else {
			ManeuverShip();
			
			if (gameManager.JumpIsReady) {
				if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.J)) {
					gameManager.PlayerHasWon = true;
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyLaser enemyLaser = collider.GetComponent<EnemyLaser>();
		Asteroid asteroid = collider.GetComponent<Asteroid>();
		PowerUp powerUp = collider.GetComponent<PowerUp>();
		
		if (enemyLaser) {
			if (!shield.shieldIsDown()) {
				shield.HitWith(enemyLaser.DamagePoints);
			} else {
				HitWith(enemyLaser.DamagePoints);
			}
		}
		
		if (asteroid) {
			if (!shield.shieldIsDown()) {
				shield.DestroyShield();
			} else {
				HitWith(currentHitPoints);
			}
		}
		
		if (powerUp) {
			GameObject itemObject = powerUp.PowerUpItem;
			PlayerLaser laser = itemObject.GetComponent<PlayerLaser>();
			if (laser) {
				gun.LaserObject = itemObject;
			} else {
				gun.TorpedoObject = itemObject;
				gun.TorpedoesLeft = 5;
			}
			Destroy(powerUp.gameObject);
		}
	}
	#endregion
	
	
	#region Private Methods
	void CalculateCameraDistance() {
		Hashtable b = GameCamera.GetBoundaries(Camera.main, transform, shipPadding);
	
		minX = (float) b["minX"];
		maxX = (float) b["maxX"];
	}

	void FtlJump ()	{
		PlayerLaser[] lasers = FindObjectsOfType<PlayerLaser> ();
		if (0 == lasers.Length) {
			exitTarget = new Vector3(transform.position.x, exitTargetY, transform.position.z);
			WarpOut ();
			if (transform.position == exitTarget) {
				levelManager.LoadNextLevel ();
			}
		}
	}
	
	void HitWith(float damage) {
		currentHitPoints -= damage;
		if (0f <= currentHitPoints) {
			Destroy(gameObject);
			levelManager.LoadLevel("04 Lose Screen");
		}
	}

	void ManeuverShip() {
		Vector3 shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float newXPosition = shipPosition.x;
		float newYPosition = shipPosition.y;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			newXPosition = transform.position.x - shipSpeed * Time.deltaTime;
		}
		else
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
				newXPosition = transform.position.x + shipSpeed * Time.deltaTime;
			}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			newYPosition = transform.position.y + shipSpeed * Time.deltaTime;
		}
		else
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
				newYPosition = transform.position.y - shipSpeed * Time.deltaTime;
			}
		UpdateShipPosition(shipPosition, newXPosition, newYPosition);
	}
	
	void UpdateShipPosition(Vector3 ship, float x, float y) {
		ship.x = Mathf.Clamp(x, minX, maxX);
		ship.y = Mathf.Clamp(y, minY, maxY);
		transform.position = ship;
	}
	
	void WarpIn() {
		float step = spawnWarpSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, spawnInTarget, step);
	}
	
	void WarpOut() {
		float step = exitWarpSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, exitTarget, step);
	}
	#endregion
	
	
	public void LoadNextLevel() {
		gameManager.PlayerHasWon = true;
	}
}
