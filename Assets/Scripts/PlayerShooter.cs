using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {

	[SerializeField]
	private GameObject weaponPrefab;
	
	[SerializeField]
	private float defaultProjectileSpeed = 10f;
	private float currentProjectileSpeed;
	
	[SerializeField]
	private float defaultProjectileRate = 0.2f;
	private float currentProjectileRate;
	
	private GameManager gameManager;
	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		currentProjectileRate  = defaultProjectileRate;
		currentProjectileSpeed = defaultProjectileSpeed;
	}
	
	void Update() {
		if (!gameManager.PlayerIsSpawning) {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
				StartFiringPrimaryWeapon();
			}
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) {
				StopFiringPrimaryWeapon();
			}
			if (Input.GetMouseButton(1)) {
				Debug.Log("Mouse button 1 pressed.");
			}
		}
	}
	
	void FirePrimaryWeapon() {
		GameObject laser = Instantiate(weaponPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void FireSecondaryWeapon() {
		
	}
	
	public void StartFiringPrimaryWeapon() {
		InvokeRepeating("FirePrimaryWeapon", 0.0001f, currentProjectileRate);
	}
	
	public void StopFiringPrimaryWeapon() {
		CancelInvoke("FirePrimaryWeapon");
	}
}
