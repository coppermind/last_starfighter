using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class StoryController : MonoBehaviour {

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
			CloseStoryPanel();
		}
		
		string filename = "Assets/Story/02 Level 01/00.txt";
		if (File.Exists(filename)) {
//			Debug.Log("File - " + filename + " exists!");
			byte[] contents = File.ReadAllBytes(filename);
			string c = contents.ToString();
//			Debug.Log(c);
		} else {
//			Debug.Log("File - " + filename + " missing!");
		}
		
		float test = 100;
		
	}
	
	public void Continue() {
		if (currentTextIndex >= text.Length-1) {
			CloseStoryPanel();
		}
	}
	
	public void OpenStoryPanel() {
		gameManager.PauseGame();
	}
	
	public void CloseStoryPanel() {
		gameObject.SetActive(false);
		gameManager.UnpauseGame();
	}
}
