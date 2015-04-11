using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int autoloadNextLevelInSeconds;
	public int fadeTransitionInSeconds;
	
	private bool isFadingIn = true;
	private bool isFadingOut = false;
	
	private string nextLevelName = null;
	private int nextLevelNumber = 0;
	
	private GameObject fadePanel;
	
	void Start() {
		fadePanel = GameObject.Find("Fade Panel");
		
		isFadingIn = true;
		
		if (0 < autoloadNextLevelInSeconds) {
			Invoke("LoadNextLevel", autoloadNextLevelInSeconds);
		}
	}
	
	void Update() {
		if (isFadingIn) {
			FadeIn();
		} else if (isFadingOut) {
			FadeOut();
		}
	}
	
	void FadeIn() {
		float step = fadeTransitionInSeconds * Time.deltaTime;
		Image panelImage = fadePanel.GetComponent<Image>();
		float alpha = panelImage.color.a - step;
		panelImage.color = new Color(0, 0, 0, alpha);
		if (alpha <= 0) {
			fadePanel.SetActive(false);
			isFadingIn = false;
		}
	}
	
	void FadeOut() {
		fadePanel.SetActive(true);
		float step = fadeTransitionInSeconds * Time.deltaTime;
		Image panelImage = fadePanel.GetComponent<Image>();
		float alpha = panelImage.color.a + step;
		panelImage.color = new Color(0, 0, 0, alpha);
		if (alpha >= 1) {
			isFadingOut = false;
			GotoLevel();
		}
	}
	
	void GotoLevel() {
		if (nextLevelName != null) {
			Application.LoadLevel(nextLevelName);
		} else {
			Application.LoadLevel(nextLevelNumber);
		}
	}
	
	void OnLevelWasLoaded() {
		// Reset score and player lives if in Main Menu
		if (Application.loadedLevel == 1) {
			PlayerScore.ResetScore();
			GameManager.currentLivesCount = GameManager.totalPlayerLives;
		}
	}
	
	public void LoadLevel(string name) {
		nextLevelName = name;
		isFadingOut = true;
	}
	
	public void LoadNextLevel() {
		nextLevelNumber = Application.loadedLevel + 1;
		isFadingOut = true;
	}

	public void QuitGame() {
		Debug.Log("Quit game request sent.");
		Application.Quit();
	}
}
