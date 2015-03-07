using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MUSIC_VOLUME_KEY   = "music_volume";
	const string EFFECTS_VOLUME_KEY = "effects_volume";
	const string DIFFICULTY_KEY     = "game_difficulty";
	const string LEVEL_KEY          = "level_unlocked_"; // level_unlocked_N
	const float  DEFAULT_VOLUME     = 0.5f;
	const int  DEFAULT_DIFFICULTY   = 1;
	
	public static void SetMusicVolume(float amount) {
		if (0f <= amount && 1f >= amount) {
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, amount);
		} else {
			Debug.LogWarning("Set music volume is out of range. (" + amount + ")");
		}
	}
	
	public static float GetMusicVolume() {
		if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY)) {
			return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
		} else {
			return DEFAULT_VOLUME;
		}
	}
	
	public static void SetEffectsVolume(float amount) {
		if (0f <= amount && 1f >= amount) {
			PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, amount);
		} else {
			Debug.LogWarning("Set effects volume is out of range. (" + amount + ")");
		}
	}
	
	public static float GetEffectsVolume() {
		if (PlayerPrefs.HasKey(EFFECTS_VOLUME_KEY)) {
			return PlayerPrefs.GetFloat(EFFECTS_VOLUME_KEY);
		} else {
			return DEFAULT_VOLUME;
		}
	}
	
	public static void SetDifficulty(int difficulty) {
		if (0 < difficulty && 4 > difficulty) {
			PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogWarning("Set difficulty is out of range. (" + difficulty + ")");
		}
	}
	
	public static int GetDifficulty() {
		if (PlayerPrefs.HasKey(DIFFICULTY_KEY)) {
			return PlayerPrefs.GetInt(DIFFICULTY_KEY);
		} else {
			return DEFAULT_DIFFICULTY;
		}
	}
	
	public static void UnlockLevel(int level) {
		if ((Application.levelCount - 1) >= level) {
			PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
		} else {
			Debug.LogWarning("Level " + level + " is not in the build order");
		}
	}
	
	public static bool IsLevelUnlocked(int level) {
		if ((Application.levelCount - 1) >= level) {
			return (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) > 0);
		} else {
			Debug.LogWarning("Level " + level + " is not in the build order");
		}
		return false;
	}
	
	public static float GetDefaultMusicVolume() {
		return DEFAULT_VOLUME;
	}
	
	public static float GetDefaultEffectsVolume() {
		return DEFAULT_VOLUME;
	}
	
	public static float GetDefaultDifficulty() {
		return DEFAULT_DIFFICULTY;
	}
}
