using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static int totalPlayerLives = 3;
	public static int currentLivesCount = 3;
	
	public static GameObject playerLaser;
	public static GameObject playerTorpedo;
	
	LevelManager levelManager;
	
	bool gameIsPaused   = false;
	bool jumpIsReady    = false;
	bool playerHasWon   = false;
	bool playerIsDead   = false;
	bool playerIsGod    = false;
	bool playerSpawning = false;
	
	public GameObject playerShipPrefab;
	public Vector3 playerShipSpawnVector;
	
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	#region Properties
	public bool GameIsPaused { 
		get { return gameIsPaused; }
		set { gameIsPaused = value; }
	}
	
	public bool JumpIsReady {
		get { return jumpIsReady; }
		set { jumpIsReady = value; }
	}
	
	public bool PlayerHasWon {
		get { return playerHasWon; }
		set { playerHasWon = value; }
	}
	
	public bool PlayerIsDead {
		get { return playerIsDead; }
		set { playerIsDead = value; }
	}
	
	public bool PlayerIsInvincible {
		get { return playerIsGod; }
		set { playerIsGod = value; }
	}
	
	public bool PlayerIsSpawning {
		get { return playerSpawning; }
		set { playerSpawning = value; }
	}
	#endregion
	
	public void PauseGame() {
		GameIsPaused = true;
	}
	
	public void UnpauseGame() {
		GameIsPaused = false;
	}
	
	public void PlayerDied() {
		if (0 >= currentLivesCount) {
			levelManager.LoadLevel("04 Lose Screen");
		} else {
			SpawnNewPlayerShip();
			currentLivesCount--;
		}
		GameManager.playerLaser = null;
		GameManager.playerTorpedo = null;
	}
	
	void SpawnNewPlayerShip() {
		Instantiate(playerShipPrefab, playerShipSpawnVector, Quaternion.identity);
	}

}
