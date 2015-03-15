using UnityEngine;
using System.Collections;

public class UfoShip : MonoBehaviour {

	public enum DIRECTION {RIGHT, LEFT};

	[SerializeField]
	private float shipSpeed = 4f;

	private float shipDirection = -1;

	[SerializeField]
	private GameObject itemPrefab;
	
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
		GameObject playerLaser = collider.GetComponent<PlayerLaser>();
		
		if (playerLaser) {
			// Die and spawn prefabbed item
		}
	}
	
	void GetVelocity() {
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
