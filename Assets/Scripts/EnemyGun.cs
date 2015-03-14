using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

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
	}
	
	void Update() {
		if (gameManager.GameIsPaused) {
			return;
		}
		
		if (!gameManager.PlayerIsSpawning) {
			if (GameMath.IsProbable(currentProjectileRate)) {
				FirePrimaryWeapon();
			}
		}
	}
	#endregion
	
	
	#region Private Methods
	void FirePrimaryWeapon() {
		Instantiate(weaponPrefab, transform.position, Quaternion.identity);
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
