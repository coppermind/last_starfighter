using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int autoloadNextLevelInSeconds;
	public float levelTransition;

	void Start() {
		if (0 < autoloadNextLevelInSeconds) {
			Invoke("LoadNextLevel", autoloadNextLevelInSeconds);
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
