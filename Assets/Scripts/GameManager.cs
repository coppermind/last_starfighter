using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool paused = false;
	
	public void PauseGame() {
		paused = true;
	}
	
	public void UnpauseGame() {
		paused = false;
	}
	
	public bool IsPaused() {
		return paused;
	}

}
