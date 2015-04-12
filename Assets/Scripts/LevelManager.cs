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
	
	[SerializeField]
	private GameObject fadePanelPrefab;
	private GameObject fadePanel;
	
	void Start() {
		CreateFadePanel("fade in");
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
			GameManager.playerLaser = null;
			GameManager.playerTorpedo = null;
		}
	}
	
	#region FadePanel Methods
	void CreateFadePanel(string forAction) {
		GameObject uiCanvas = GameObject.Find("UI Canvas");
		fadePanel = Instantiate(fadePanelPrefab);
		fadePanel.transform.SetParent(uiCanvas.transform, false);
		
		float alpha = 1f;
		if (forAction == "fade out") { alpha = 0f; }
		
		Image panelImage = fadePanel.GetComponent<Image>();
		panelImage.color = new Color(0f, 0f, 0f, alpha);
		
		
	}
	
	void DestroyFadePanel() {
		Destroy(fadePanel.gameObject);
		fadePanel = null;
	}
	
	void FadeIn() {
		float step = fadeTransitionInSeconds * Time.deltaTime;
		Image panelImage = fadePanel.GetComponent<Image>();
		float alpha = panelImage.color.a - step;
		panelImage.color = new Color(0, 0, 0, alpha);
		if (alpha <= 0) {
			DestroyFadePanel();
			isFadingIn = false;
		}
	}
	
	void FadeOut() {
		if (!fadePanel) {
			CreateFadePanel("fade out");
		}
		float step = fadeTransitionInSeconds * Time.deltaTime;
		Image panelImage = fadePanel.GetComponent<Image>();
		float alpha = panelImage.color.a + step;
		panelImage.color = new Color(0, 0, 0, alpha);
		if (alpha >= 1) {
			isFadingOut = false;
			GotoLevel();
		}
	}
	#endregion
	
	
	#region Public Members
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
	#endregion
}
