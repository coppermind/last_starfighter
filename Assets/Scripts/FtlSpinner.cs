using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FtlSpinner : MonoBehaviour {

	public float spinSeconds = 20f;
	
	float originalWidth = 0f;
	
	RectTransform rectTrans;
	
	Transform statusObject, statusLabel;
	
	GameManager gameManager;
	
	void Start () {
		gameManager  = FindObjectOfType<GameManager>();
		
		statusObject = transform.Find("Status");
		statusLabel  = transform.Find("Status Label");
		
		rectTrans = statusObject.GetComponent<RectTransform>();
		originalWidth = rectTrans.sizeDelta.x;
	}
	
	void Update () {
		if (!gameManager.GameIsPaused && !gameManager.PlayerIsSpawning) {
			float currentWidth = rectTrans.sizeDelta.x;
			
			if (currentWidth < 0) {
				float step = originalWidth / spinSeconds * Time.deltaTime;
				
				currentWidth -= step;
				rectTrans.sizeDelta = new Vector2(currentWidth, 0);
			} else {
				Animator animator = statusLabel.GetComponent<Animator>();
				Text text = statusLabel.GetComponent<Text>();
				
				text.text = "FTL Ready";
				animator.SetTrigger("SpinnerReady trigger");
				
				gameManager.JumpIsReady = true;
			}
		}
	}
}
