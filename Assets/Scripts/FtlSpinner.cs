using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FtlSpinner : MonoBehaviour {

	[SerializeField]
	private float spinSeconds = 20f;
	private float originalWidth = 0f;
	
	private RectTransform rectTrans;
	
	private Transform statusObject;
	private Transform statusLabel;
	private GameManager gameManager;
	
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
