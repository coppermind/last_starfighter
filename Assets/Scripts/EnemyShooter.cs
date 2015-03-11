using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	[SerializeField]
	private GameObject weaponPrefab;
	
	[SerializeField]
	private float defaultProjectileSpeed = -10f;
	
	[SerializeField]
	private float defaultProjectileRate = 0.2f;
	
	private float currentProjectileSpeed;
	private float currentProjectileRate;
	
	void Start() {
		currentProjectileRate  = defaultProjectileRate;
		currentProjectileSpeed = defaultProjectileSpeed;
	}
	
	void Update() {
		float probability = currentProjectileRate * Time.deltaTime;
		if (Random.value < probability) {
			FirePrimaryWeapon();
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
