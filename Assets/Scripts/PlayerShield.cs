using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public Sprite[] shieldLevels;
	public float totalHitPoints;
	public float regenerationRate;
	
	private float currentHitPoints;
	private SpriteRenderer spriteRenderer;

	void Start() {
		currentHitPoints = totalHitPoints;
		spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}
	
	void Update() {
		if (0f < regenerationRate && currentHitPoints < totalHitPoints) {
			currentHitPoints += regenerationRate * Time.deltaTime;
			UpdateSprite();
		} else {
			Debug.Log("Not regenerating shield");
		}
	}
	
	void HitWith(float damage) {
		totalHitPoints -= damage;
		UpdateSprite();
	}
	
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
	
	public bool shieldIsDown() {
		return 0f <= currentHitPoints;
	}
}
