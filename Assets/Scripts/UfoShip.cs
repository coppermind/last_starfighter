using UnityEngine;
using System.Collections;

public class UfoShip : MonoBehaviour {

	public enum DIRECTION {RIGHT, LEFT};

	[SerializeField]
	private float shipSpeed = 4f;

	private float shipDirection = -1;

	[SerializeField]
	private GameObject itemPrefab;
	
	[SerializeField]
	private GameObject explosionPrefab;
	
	private Rigidbody2D rigidBody;
	
	private GameManager gameManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning) {
			rigidBody.velocity = new Vector3(0f, 0f, 0f);
			return;
		}
		
		rigidBody.velocity = GetVelocity();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerLaser playerLaser = collider.GetComponent<PlayerLaser>();
		
		if (playerLaser) {
			DieAndSpawnItem();
		}
	}
	
	void DieAndSpawnItem() {
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.parent = transform;
		
		Transform body = transform.Find("Body");
		if (body) { 
			Destroy(body.gameObject);
		}
		Destroy(gameObject, 0.4f);
		
		if (itemPrefab) {
			// Spawn item
			Instantiate(itemPrefab, transform.position, Quaternion.identity);
		}
	}
	
	Vector3 GetVelocity() {
		float speed = shipSpeed * shipDirection;
		return new Vector3(speed , 0f, 0f);
	}
	
	public void SetDirect(DIRECTION direction) {
		if (direction == DIRECTION.LEFT) {
			shipDirection = -1;
		} else {
			shipDirection = 1;
		}
	}
}
