using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	#region Gameplay Members
	[SerializeField]
	private Sprite[] shieldLevels;
	
	[SerializeField]
	private float totalHitPoints;
	private float currentHitPoints;
	
	[SerializeField]
	private float regenerationRate;
	#endregion
	
	
	#region Component Members
	private SpriteRenderer spriteRenderer;
	#endregion

	
	#region Unity Methods
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
			//Debug.Log("Not regenerating shield");
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
