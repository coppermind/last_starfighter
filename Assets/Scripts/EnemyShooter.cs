using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	#region Transform Members
	[SerializeField]
	private float defaultProjectileSpeed = -10f;
	private float currentProjectileSpeed;
	#endregion


	#region Gameplay Members
	[SerializeField]
	private GameObject weaponPrefab;
	
	[SerializeField]
	private float defaultProjectileRate = 0.2f;
	private float currentProjectileRate;
	#endregion
	
	
	#region GameObject Members
	private GameManager gameManager;
	#endregion

	
	#region Unity Methods	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		currentProjectileRate  = defaultProjectileRate;
		currentProjectileSpeed = defaultProjectileSpeed;
	}
	
	void Update() {
		if (!gameManager.PlayerIsSpawning) {
			if (GameMath.IsProbable(currentProjectileRate)) {
				FirePrimaryWeapon();
			}
		}
	}
	#endregion
	
	
	#region Private Methods
	void FirePrimaryWeapon() {
		GameObject laser = Instantiate(weaponPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, currentProjectileSpeed, 0f);
	}
	
	void FireSecondaryWeapon() {
		
	}
	#endregion
	
	
	#region Public Methods
	public void StartFiringPrimaryWeapon() {
		InvokeRepeating("FirePrimaryWeapon", 0.0001f, currentProjectileRate);
	}
	
	public void StopFiringPrimaryWeapon() {
		CancelInvoke("FirePrimaryWeapon");
	}
	#endregion	
}
