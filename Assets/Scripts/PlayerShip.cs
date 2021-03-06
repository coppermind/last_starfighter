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
	
	[SerializeField]
	AudioClip hitClip;
	
	[SerializeField]
	AudioClip explodeClip;
	
	[SerializeField]
	AudioClip powerUpClip;
	
	[SerializeField]
	AudioClip ftlJumpClip;
	
	[SerializeField]
	float invincibleInSeconds = 5f;
	float startTime;
	
	[SerializeField]
	float explosionLengthInSeconds = 1f;
	#endregion


	#region Component Members
	Animator animator;
	
	PlayerBody body;
	PlayerGun gun;
	PlayerShield shield;
	
	CircleCollider2D shipCollider;
	
	AudioSource audioSource;
	#endregion

		
	#region GameObject Members
	LevelManager levelManager;
	
	GameManager gameManager;
	
	[SerializeField]
	GameObject explosionPrefab;
	
	PlayerScore playerScore;
	#endregion
	
	
	#region Unity Methods
	void Start() {
		levelManager = FindObjectOfType<LevelManager>();
		gameManager  = FindObjectOfType<GameManager>();
		playerScore  = FindObjectOfType<PlayerScore>();
		
		body         = GetComponentInChildren<PlayerBody>();
		gun          = GetComponentInChildren<PlayerGun>();
		shield       = GetComponentInChildren<PlayerShield>();

		shipCollider = GetComponent<CircleCollider2D>();
		shipCollider.enabled = false;
		
		audioSource  = GetComponent<AudioSource>();
		
		gameManager.PlayerIsSpawning = true;
		gameManager.PlayerIsDead     = false;
		gameManager.PlayerIsInvincible = true;
		
		startTime = Time.time;
		
		spawnInTarget = new Vector3(spawnTargetX, spawnTargetY, transform.position.z);
		
		currentHitPoints = hitPoints * DifficultyModifier.ForPlayerHitPoints();
		CalculateCameraDistance();
	}

	void Update() {
		if (gameManager.GameIsPaused) { return; }
		
		if (gameManager.PlayerIsSpawning) {
			WarpIn();
			
			if (transform.position == spawnInTarget) {
				gameManager.PlayerIsSpawning = false;
			}
			
		} else if (gameManager.PlayerHasWon) {
			FtlJump();
		
		} else {
			ManeuverShip();
			
			if (gameManager.JumpIsReady) {
				if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.J)) {
					StartFtlJump();
				}
			}
		}
		
		if (gameManager.PlayerIsInvincible) {
			float elapsedTime = Time.time - startTime;
			if (elapsedTime >= invincibleInSeconds) {
				shipCollider.enabled = true;
				gameManager.PlayerIsInvincible = false;
				body.MakeVulnerable();
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Asteroid asteroid           = collider.GetComponent<Asteroid>();
		BomberShip bomberShip       = collider.GetComponent<BomberShip>();
		BomberTorpedo bomberTorpedo = collider.GetComponent<BomberTorpedo>();
		EnemyLaser enemyLaser       = collider.GetComponent<EnemyLaser>();
		EnemyShip enemyShip         = collider.GetComponent<EnemyShip>();
		PowerUp powerUp             = collider.GetComponent<PowerUp>();
		
		if (bomberTorpedo || enemyLaser) {
			float damage = (enemyLaser) ? enemyLaser.DamagePoints : bomberTorpedo.DamagePoints;
			if (shield.shieldIsDown()) {
				HitWith(damage);
			} else {
				shield.HitWith(damage);
			}
		}
		
		if (asteroid || bomberShip || enemyShip) {
			if (shield.shieldIsDown()) {
				HitWith(currentHitPoints);
			} else {
				shield.DestroyShield();
			}
			
			if (asteroid)   { asteroid.Destroy(); }
			if (bomberShip) { bomberShip.HitWith(); }
			if (enemyShip)  { enemyShip.HitWith(); }
		}
		
		if (powerUp) { PickupItem(powerUp); }
	} 
	#endregion
	
	
	#region Private Methods
	void CalculateCameraDistance() {
		Hashtable b = GameCamera.GetBoundaries(Camera.main, transform, shipPadding);
	
		minX = (float) b["minX"];
		maxX = (float) b["maxX"];
	}
	
	void PlayAudio() {
		audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
		audioSource.Play();
	}

	void FtlJump()	{
		PlayerLaser[] lasers = FindObjectsOfType<PlayerLaser>();
		if (0 == lasers.Length) {
			exitTarget = new Vector3(transform.position.x, exitTargetY, transform.position.z);
			WarpOut();
			if (transform.position == exitTarget) {
				levelManager.LoadNextLevel();
			}
		}
	}
	
	void HitWith(float damage) {
		currentHitPoints -= damage;
		if (0f >= currentHitPoints) {
			gameManager.PlayerIsDead = true;
			
			CircleCollider2D collider = GetComponent<CircleCollider2D>();
			collider.enabled = false;
			
			body.Kill();
			
			GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
			explosion.transform.parent = transform;
			
			StartCoroutine("DestroyObject");
			
			audioSource.clip = explodeClip;
		} else {
			audioSource.clip = hitClip;
		}
		PlayAudio();
	}
	
	IEnumerator DestroyObject() {
		yield return new WaitForSeconds(explosionLengthInSeconds);
		Destroy(gameObject);
		gameManager.PlayerDied();
	}

	void ManeuverShip() {
		Transform body = transform.Find("Body");
		if (!body) { return; }
		
		Vector3 shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float newXPosition = shipPosition.x;
		float newYPosition = shipPosition.y;
		
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			newXPosition = transform.position.x - CurrentSpeed() * Time.deltaTime;
		} else {
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
				newXPosition = transform.position.x + CurrentSpeed() * Time.deltaTime;
			}
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			newYPosition = transform.position.y + CurrentSpeed() * Time.deltaTime;
		} else {
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
				newYPosition = transform.position.y - CurrentSpeed() * Time.deltaTime;
			}
		}
		UpdateShipPosition(shipPosition, newXPosition, newYPosition);
	}

	void PickupItem(PowerUp powerUp) {
		GameObject itemObject = powerUp.PowerUpItem;
		PlayerLaser laser = itemObject.GetComponent<PlayerLaser>();
		if (laser) {
			gun.LaserObject = itemObject;
			GameManager.playerLaser = itemObject;
		}
		else {
			gun.TorpedoObject = itemObject;
			gun.TorpedoesLeft = 5;
			GameManager.playerTorpedo = itemObject;
		}
		Destroy(powerUp.gameObject);
		playerScore.AddScore(powerUp.scorePoints);
		audioSource.clip = powerUpClip;
		audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
		PlayAudio();
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
	
	#region Difficulty Modifiers
	float CurrentSpeed() {
		return shipSpeed * DifficultyModifier.ForPlayerSpeed();
	}
	#endregion
	
	public void StartFtlJump() {
		shipCollider.enabled = false;
		gameManager.PlayerHasWon = true;
		audioSource.clip = ftlJumpClip;
		PlayAudio();
	}
}
