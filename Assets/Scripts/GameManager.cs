using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool paused = false;
	private bool playerSpawning = true;
	
	public void PauseGame() {
		paused = true;
	}
	
	public void UnpauseGame() {
		paused = false;
	}
	
	public bool IsPaused() {
		return paused;
	}
	
	public bool PlayerIsSpawning {
		get { return playerSpawning; }
		set { playerSpawning = value; }
	}

}
