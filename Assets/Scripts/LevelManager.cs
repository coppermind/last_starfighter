using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private int autoloadNextLevelInSeconds;
	
	[SerializeField]
	private float levelTransition;

	void Start() {
		if (0 < autoloadNextLevelInSeconds) {
			Invoke("LoadNextLevel", autoloadNextLevelInSeconds);
		}
	}
	
	void OnLevelWasLoaded() {
		// Reset score if in Main Menu
		if (Application.loadedLevel == 1) {
			PlayerScore.ResetScore();
		}
	}
	
	public void LoadLevel(string name) {
		Application.LoadLevel(name);
	}
	
	public void LoadNextLevel() {
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void QuitGame() {
		Debug.Log("Quit game request sent.");
		Application.Quit();
	}
}
