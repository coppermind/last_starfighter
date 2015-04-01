using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Net;

public class StoryController : MonoBehaviour {

//	public DialogueObject dialogue;

	public string[] text;

	[SerializeField]
	Text displayText;
	
	int currentTextIndex;
	
	string content;

	GameManager gameManager;
	
	void Awake () {
		WWW www = new WWW("http://coppermind.io/");
		StartCoroutine( LoadWeb(www) );
	}

	void Start () {
		Debug.Log("Story Controller Start()!!!!!!!!!!");
		gameManager = FindObjectOfType<GameManager>();
		gameManager.PauseGame();
		
		if (0 < text.Length) {
			currentTextIndex = 0;
			displayText.text = text[0];
		} else {
			CloseStoryPanel();
		}
		
//		string filename = "Assets/Story/02 Level 01/00.txt";
//		if (File.Exists(filename)) {
//			Debug.Log("File - " + filename + " exists!");
//			string contents = File.ReadAllText(filename);
//			displayText.text = contents;
//			Debug.Log(contents);
//		} else {
//			Debug.Log("File - " + filename + " missing!");
//		}
	}
	
	IEnumerator LoadWeb(WWW www) {
		yield return www;
		
		if (www.error == null) {
			displayText.text = www.text;
		} else {
			Debug.Log("WWW Error: " + www.error);
		}
	}
	
	public void Next() {
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
