using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float shipSpeed = 8f;
	public float shipPadding = 0.5f;
	
	public float defaultProjectileSpeed = 10f;
	public float defaultProjectileRate = 0.2f;
	
	public float hitPoints = 100f;
	public int numberOfLives = 3;

	public AudioClip defaultEffects;
	
	private float minX, maxX, minY, maxY;
	private LevelManager levelManager;

	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		
		minX = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + shipPadding;
		minY = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + shipPadding;
		
		maxX = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - shipPadding;
		maxY = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y - shipPadding;
		
		Debug.Log("maxX: " + maxX.ToString());
	}
	
	void Update () {
		Vector3 shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		float newXPosition = shipPosition.x;
		float newYPosition = shipPosition.y;
		
		if (Input.GetKey(KeyCode.A)) {
			newXPosition = transform.position.x - shipSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			newXPosition = transform.position.x + shipSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)) {
			newYPosition = transform.position.y + shipSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S)) {
			newYPosition = transform.position.y - shipSpeed * Time.deltaTime;
		}
		UpdateShipPosition(shipPosition, newXPosition, newYPosition);
		
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Mouse button 0 pressed.");
		}
		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("Mouse button 0 released.");
		}
	}
	
	void UpdateShipPosition(Vector3 ship, float x, float y) {
		ship.x = Mathf.Clamp(x, minX, maxX);
		ship.y = Mathf.Clamp(y, minY, maxY);
		transform.position = ship;
	}
	
}
