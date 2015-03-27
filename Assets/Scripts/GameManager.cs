using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	bool gamePaused = false;
	bool playerSpawnIn = true;
	bool playerWins = false;
	bool ftlSpinnerReady = false;
	
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

}
