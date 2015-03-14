using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	[SerializeField]
	private string[] text;

	[SerializeField]
	private Text displayText;

	private int currentTextIndex;

	private GameManager gameManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		gameManager.PauseGame();
		
		if (0 < text.Length) {
			currentTextIndex = 0;
			displayText.text = text[0];
		} else {
			CloseTextPanel();
		}
	}
	
	public void Continue() {
		if (currentTextIndex >= text.Length-1) {
			CloseTextPanel();
		}
	}
	
	public void CloseTextPanel() {
		gameObject.SetActive(false);
		gameManager.UnpauseGame();
	}
}
