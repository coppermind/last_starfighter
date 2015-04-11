using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FtlSpinner : MonoBehaviour {

	public float spinSeconds = 20f;
	float currentSpinSeconds;
	
	float originalWidth = 0f;
	
	RectTransform rectTrans;
	
	Transform statusObject, statusLabel;
	
	AudioSource audioSource;
	
	GameManager gameManager;
	
	void Start () {
		gameManager  = FindObjectOfType<GameManager>();
		
		audioSource  = GetComponent<AudioSource>();
		
		statusObject = transform.Find("Status");
		statusLabel  = transform.Find("Status Label");
		
		rectTrans = statusObject.GetComponent<RectTransform>();
		originalWidth = rectTrans.sizeDelta.x;
		
		currentSpinSeconds = spinSeconds * DifficultyModifier.ForFtlTimer();
	}
	
	void Update () {
		if (gameManager.GameIsPaused || gameManager.PlayerIsSpawning) {
			return;
		}
		
		float currentWidth = rectTrans.sizeDelta.x;
		
		if (currentWidth < 0) {
			float step = originalWidth / currentSpinSeconds * Time.deltaTime;
			
			currentWidth -= step;
			rectTrans.sizeDelta = new Vector2(currentWidth, 0);
		} else {
			if (!gameManager.JumpIsReady) {
				Animator animator = statusLabel.GetComponent<Animator>();
				Text text = statusLabel.GetComponent<Text>();
				
				text.text = "FTL Ready";
				animator.SetTrigger("SpinnerReady trigger");
				
				gameManager.JumpIsReady = true;
				
				audioSource.volume = PlayerPrefsManager.GetEffectsVolume();
				audioSource.Play();
			}
		}
	}
}
