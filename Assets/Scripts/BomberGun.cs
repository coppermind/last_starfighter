using UnityEngine;
using System.Collections;

public class BomberGun : MonoBehaviour {

	[SerializeField]
	GameObject weaponPrefab;

	public int barrageTotalProjectiles = 3;
	int currentBarrageCount = 0;
	
	public float firingRate = 4f;
	
	public float defaultProjectileRate = 0.2f;
	float currentProjectileRate;
	
	bool isFiring = false;
	
	GameManager gameManager;
	
	AudioSource audioSource;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		
		audioSource = GetComponent<AudioSource>();
		
		currentProjectileRate  = defaultProjectileRate;
		currentBarrageCount = 0;
	}
	
	void Update () {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning || isFiring) {
			return;
		}
		
		if (GameMath.IsProbable(CurrentFiringRate())) {
			StartFiringWeapon();
		}
	}
	
	void FireWeapon() {
		Instantiate(weaponPrefab, transform.position, Quaternion.identity);
		
		audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
		audioSource.Play();
		
		currentBarrageCount++;
		if (currentBarrageCount >= TotalTorpedoes()) {
			StopFiringWeapon();
		}
	}
	
	void StartFiringWeapon() {
		isFiring = true;
		InvokeRepeating("FireWeapon", 0.5f, currentProjectileRate);
	}
	
	void StopFiringWeapon() {
		isFiring = false;
		currentBarrageCount = 0;
		CancelInvoke("FireWeapon");
	}
	
	int TotalTorpedoes() {
		return barrageTotalProjectiles + (int) DifficultyModifier.ForBomberTorpedoBarrage();
	}
	
	float CurrentFiringRate() {
		return firingRate * DifficultyModifier.ForBomberTorpedoBarrageRate();
	}
}
