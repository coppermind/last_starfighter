using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {

	public GameObject weaponPrefab;
	
	public float defaultProjectileSpeed = 10f;
	public float defaultProjectileRate = 0.2f;
	
	private float currentProjectileSpeed;
	private float currentProjectileRate;
	
	void Start() {
		currentProjectileRate  = defaultProjectileRate;
		currentProjectileSpeed = defaultProjectileSpeed;
	}
	
	void Update() {
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
	
	public void StartFiringPrimaryWeapon() {
		InvokeRepeating("FirePrimaryWeapon", 0.0001f, currentProjectileRate);
	}
	
	public void StopFiringPrimaryWeapon() {
		CancelInvoke("FirePrimaryWeapon");
	}
	
	void FirePrimaryWeapon() {
		GameObject laser = Instantiate(weaponPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void FireSecondaryWeapon() {
	
	}
}
