using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static int totalPlayerLives = 3;
	public static int currentLivesCount = 3;
	
	bool gamePaused = false;
	bool playerSpawnIn = true;
	bool playerWins = false;
	bool ftlSpinnerReady = false;
	
	LevelManager levelManager;
	
	public GameObject playerShipPrefab;
	public Vector3 playerShipSpawnVector;
	
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	public void PauseGame() {
		gamePaused = true;
	}
	
	public void UnpauseGame() {
		gamePaused = false;
	}
	
	public bool GameIsPaused {
		get { return gamePaused; }
		set { gamePaused = value; }
	}
	
	public bool PlayerIsSpawning {
		get { return playerSpawnIn; }
		set { playerSpawnIn = value; }
	}
	
	public bool PlayerHasWon {
		get { return playerWins; }
		set { playerWins = value; }
	}
	
	public bool JumpIsReady {
		get { return ftlSpinnerReady; }
		set { ftlSpinnerReady = value; }
	}
	
	public void TogglePause() {
		gamePaused = !gamePaused;
	}
	
	public void PlayerIsDead() {
		
		if (0 >= currentLivesCount) {
			levelManager.LoadLevel("04 Lose Screen");
		} else {
			SpawnNewPlayerShip();
			currentLivesCount--;
		}
	}
	
	void SpawnNewPlayerShip() {
		Instantiate(playerShipPrefab, playerShipSpawnVector, Quaternion.identity);
	}

}
