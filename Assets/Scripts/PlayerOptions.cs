using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class PlayerOptions : MonoBehaviour {

	[SerializeField]
	Slider musicSlider;
	
	[SerializeField]
	Slider effectsSlider;
	
	[SerializeField]
	Slider difficultySlider;
	
	MusicPlayer musicPlayer;
	Animator animator;
	
	GameManager gameManager;
	
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		musicPlayer = FindObjectOfType<MusicPlayer>();
		
		animator = GetComponent<Animator>();
		
		musicSlider.value      = PlayerPrefsManager.GetMusicVolume();
		effectsSlider.value    = PlayerPrefsManager.GetEffectsVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
	
	public void ShowPanel() {
		if (gameManager) {
			gameManager.PauseGame();
		}
		animator.SetTrigger("ShowOptions trigger");
	}
	
	public void HidePanel() {
		animator.SetTrigger("HideOptions trigger");
		if (gameManager) {
			gameManager.UnpauseGame();
		}
	}
	
	public void SaveMusicVolume() {
		PlayerPrefsManager.SetMusicVolume(musicSlider.value);
		if (musicPlayer) {
			musicPlayer.SetVolume(musicSlider.value);
		}
	}
	
	public void SaveEffectsVolume() {
		PlayerPrefsManager.SetEffectsVolume(effectsSlider.value);
	}
	
	public void SaveDifficulty() {
		PlayerPrefsManager.SetDifficulty((int)difficultySlider.value);
	}
	
	public void SetDefaults() {
		musicSlider.value      = PlayerPrefsManager.GetDefaultMusicVolume();
		effectsSlider.value    = PlayerPrefsManager.GetDefaultEffectsVolume();
		difficultySlider.value = PlayerPrefsManager.GetDefaultDifficulty();
		
		SaveMusicVolume();
		SaveEffectsVolume();
		SaveDifficulty();
	}
}
