using UnityEngine;
using System.Collections;

public class EnemyTorpedo : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;
	
	[SerializeField]
	private float defaultProjectileSpeed = 6f;
	float currentProjectileSpeed;
	
	float screenBottomEdge;
	
	Rigidbody2D rigidBody;
	
	GameManager gameManager;
	
	Vector3 currentTarget;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		currentTarget = GetPlayerPosition();
		
		GameObjects.SetParent(transform, "Enemy Torpedo Container");
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

		rigidBody.velocity = currentTarget;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
		
		if (player) {
			Destroy(gameObject);
		}
	}
	
	void Move() {
		float step = currentProjectileSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
	}
	
	Vector3 GetPlayerPosition() {
		PlayerShip player = FindObjectOfType<PlayerShip>() as PlayerShip;
		float x, y;
		x = player.transform.position.x - transform.position.x;
		y = player.transform.position.y - transform.position.y;
		return new Vector3(x, y, 0f);
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
