using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

	public float defaultProjectileRate = 0.2f;
	float currentProjectileRate;

	[SerializeField]
	GameObject laserPrefab;
	
	[SerializeField]
	GameObject torpedoPrefab;
	
	[SerializeField]
	int torpedoesLeft = 3;
	
	[SerializeField]
	AudioClip laserSoundFx;
	
	[SerializeField]
	AudioClip torpedoSoundFx;
	
	GameManager gameManager;
	
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		currentProjectileRate  = defaultProjectileRate;
	}
	
	void Update() {
		if (gameManager.GameIsPaused || gameManager.PlayerHasWon) {
			CancelInvoke("FirePrimaryWeapon");
			return;
		}
		
		if (!gameManager.PlayerIsSpawning && !gameManager.PlayerHasWon) {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
				StartFiringPrimaryWeapon();
			}
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) {
				StopFiringPrimaryWeapon();
			}
			if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return)) {
				FireSecondaryWeapon();
			}
		}
	}
	
	void FirePrimaryWeapon() {
		Instantiate(laserPrefab, transform.position, Quaternion.identity);
	}
	
	void FireSecondaryWeapon() {
		if (torpedoPrefab && 0 < torpedoesLeft) {
			Instantiate(torpedoPrefab, transform.position, Quaternion.identity);
			torpedoesLeft--;
		}
	}
	
	float CurrentProjectileRate() {
		return currentProjectileRate * DifficultyModifier.ForEnemyLaserRate();
	}
	
	public void StartFiringPrimaryWeapon() {
		InvokeRepeating("FirePrimaryWeapon", 0.0001f, CurrentProjectileRate());
	}
	
	public void StopFiringPrimaryWeapon() {
		CancelInvoke("FirePrimaryWeapon");
	}
	
	public int TorpedoesLeft {
		get { return torpedoesLeft; }
		set { torpedoesLeft = value; }
	}
	
	public GameObject LaserObject {
		get { return laserPrefab; }
		set { laserPrefab = value; }
	}
	
	public GameObject TorpedoObject {
		get { return torpedoPrefab; }
		set { torpedoPrefab = value; }
	}
}
