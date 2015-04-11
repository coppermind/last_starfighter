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
	AudioSource audioSource;
	
	GameManager gameManager;
	#endregion

	
	#region Unity Methods	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		audioSource = GetComponent<AudioSource>();
		
		currentProjectileRate  = defaultProjectileRate;
	}
	
	void Update() {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning) {
			return;
		}
		
		if (GameMath.IsProbable(CurrentProjectileRate())) {
			FireWeapon();
		}
	}
	#endregion
	
	
	#region Private Methods
	float CurrentProjectileRate() {
		return currentProjectileRate * DifficultyModifier.ForEnemyLaserRate();
	}
	
	void FireWeapon() {
		Instantiate(weaponPrefab, transform.position, Quaternion.identity);
		audioSource.Play();
	}
	#endregion
}
