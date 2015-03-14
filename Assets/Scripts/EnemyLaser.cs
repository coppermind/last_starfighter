using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	[SerializeField]
	private float defaultProjectileSpeed = -10f;
	private float currentProjectileSpeed;

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenBottomEdge;
	
	private Rigidbody2D rigidBody;
	
	private GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
		
		currentProjectileSpeed = defaultProjectileSpeed;
		
		rigidBody.velocity = new Vector3(0f, currentProjectileSpeed, 0f);
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
		
		rigidBody.velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
		
		if (player) {
			Destroy(gameObject);
		}
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
