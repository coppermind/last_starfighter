using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private bool gamePaused = false;
	private bool playerSpawning = true;
	
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
		get { return playerSpawning; }
		set { playerSpawning = value; }
	}
	
	public void TogglePause() {
		gamePaused = !gamePaused;
	}

}
