using UnityEngine;
using System.Collections;

public class PlayerTorpedo : MonoBehaviour {

	#region Transform Members
	public float defaultProjectileSpeed = 10f;
	float currentProjectileSpeed;
	
	float screenTopEdge;
	
	Vector3 initialVelocity;
	#endregion

	#region Gameplay Members
	public float damagePoints = 20f;

	bool autoTarget = false;
	#endregion
	
	#region Component Members
	Rigidbody2D rigidBody;
	#endregion
	
	#region GameObject Members
	public EnemyShip targetObject;
	#endregion
	
	GameManager gameManager;

		
	#region Unity Methods
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
		
		if (!autoTarget) {
			rigidBody.velocity = initialVelocity;
		} else {
			if (targetObject == null) {
				Destroy(gameObject);
			}
		}
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		GameObjects.SetParent(transform, "Player Torpedo Container");
	}
	
	void Update () {
		if (gameManager.GameIsPaused) {
			rigidBody.velocity = new Vector3(0f, 0f, 0f);
			return;
		}
		
		if (autoTarget) {
			if (!IsEnemyDead() ) {
				Move();
			} else {
				Die();
			}
		}
		
		float torpedoEdge = transform.position.y;
		if (torpedoEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		BomberShip bomber = collider.gameObject.GetComponent<BomberShip>();
		Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
		
		if (enemy || bomber) {
			Die();
			if (enemy)  { enemy.HitWith(DamagePoints); }
			if (bomber) { bomber.HitWith(DamagePoints); }
		}
		
		if (asteroid) {
			Die();
			asteroid.Destroy();
		}
	}
	#endregion
	
	
	#region Private Methods
	void Die() {
		Destroy(gameObject);
		if (targetObject) {
			targetObject.IsTargeted = false;
		}
	}
	
	void Move() {
		float step = currentProjectileSpeed * DifficultyModifier.ForPlayerTorpedoSpeed() * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
	}
	
	bool IsEnemyDead() {
		return (!targetObject);
	}

	#endregion
	
	public bool IsAutoTarget {
		get { return autoTarget; }
		set { autoTarget = value; }
	}
	
	public void SetVelocity(Vector3 velocity) {
		initialVelocity = velocity;
	}
	
	public float DamagePoints {
		get { return damagePoints * DifficultyModifier.ForPlayerTorpedoDamage(); }
		set { damagePoints = value; }
	}
}
