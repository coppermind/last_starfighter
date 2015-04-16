using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	#region Gameplay Members
	public float totalHitPoints;
	float currentHitPoints;
	
	public float regenerationRate;
	
	[SerializeField]
	Sprite[] shieldLevels;
	#endregion
	
	
	#region Component Members
	SpriteRenderer spriteRenderer;
	AudioSource audioSource;
	#endregion

	GameManager gameManager;
	
	#region Unity Methods
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource    = GetComponent<AudioSource>();
		
		currentHitPoints = totalHitPoints * DifficultyModifier.ForPlayerShieldHitPoints();
		
		UpdateSprite();
	}
	
	void Update() {
		if (gameManager.GameIsPaused || gameManager.PlayerIsDead) {
			return;
		}
		
		if (0f < regenerationRate && currentHitPoints < totalHitPoints) {
			currentHitPoints += regenerationRate * Time.deltaTime;
			UpdateSprite();
		}
	}
	#endregion
	
	
	#region Private Methods
	void UpdateSprite() {
		float level = (currentHitPoints / totalHitPoints) * 100;
		Sprite sprite = (level > 0) ? shieldLevels[1] : shieldLevels[0];
		
		float boundary = 100 / (shieldLevels.Length - 1);
		for (int i = 1; i <= shieldLevels.Length-2; i++) {
			if (level > boundary * i) {
				sprite = shieldLevels[i+1];
			}
		}
		spriteRenderer.sprite = sprite;
	}
	#endregion
	
	
	#region Public Methods
	public void DestroyShield() {
		currentHitPoints = -5f;
		UpdateSprite();
	}
	
	public void HitWith(float damage) {
		audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
		audioSource.Play();
		currentHitPoints -= damage;
		if (0f >= currentHitPoints) {
			currentHitPoints = -5f;
		}
		UpdateSprite();
	}
	
	public bool shieldIsDown() {
		return !(0f <= currentHitPoints);
	}
	#endregion
}
