using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerOptions : MonoBehaviour {

	[SerializeField]
	private Slider musicSlider;
	
	[SerializeField]
	private Slider effectsSlider;
	
	[SerializeField]
	private Slider difficultySlider;
	
	private MusicPlayer musicPlayer;
	private Animator animator;
	
	void Start () {
		animator = GetComponent<Animator>();
		musicPlayer = FindObjectOfType<MusicPlayer>();
		
		musicSlider.value = PlayerPrefsManager.GetMusicVolume();
		effectsSlider.value = PlayerPrefsManager.GetEffectsVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
	
	public void ShowPanel() {
		animator.SetTrigger("ShowOptions trigger");
	}
	
	public void HidePanel() {
		animator.SetTrigger("HideOptions trigger");
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
		musicSlider.value = PlayerPrefsManager.GetDefaultMusicVolume();
		effectsSlider.value = PlayerPrefsManager.GetDefaultEffectsVolume();
		difficultySlider.value = PlayerPrefsManager.GetDefaultDifficulty();
		
		SaveMusicVolume();
		SaveEffectsVolume();
		SaveDifficulty();
	}
}
