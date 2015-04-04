using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	#region Gameplay Members
	[SerializeField]
	GameObject weaponPrefab;
	
	public float defaultProjectileRate = 0.2f;
	float currentProjectileRate;
	#endregion
	
	
	#region GameObject Members
	GameManager gameManager;
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
			if (GameMath.IsProbable(CurrentProjectileRate())) {
				FireWeapon();
			}
		}
	}
	#endregion
	
	
	#region Private Methods
	float CurrentProjectileRate() {
		return currentProjectileRate * DifficultyModifier.ForEnemyLaserRate();
	}
	
	void FireWeapon() {
		Instantiate(weaponPrefab, transform.position, Quaternion.identity);
	}
	#endregion
}
