using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Net;
using SimpleJSON;

public class StoryController : MonoBehaviour {

	public int phase = 1;
	public int level = 1;
	private int slide = 1;
	
	private string messageText;
	//private string messageSpeaker;

	[SerializeField]
	Text displayText;
	
	int currentTextIndex;
	
	string content;

	GameManager gameManager;
	
	void Awake () {
		slide = 1;
		LoadNextSlide();
	}

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		gameManager.PauseGame();
	}
	
	void LoadNextSlide() {
		WWWForm form = new WWWForm();
		form.AddField("phase", phase);
		form.AddField("level", level);
		form.AddField("slide", slide);
		
		WWW www = new WWW("http://local.coppermind.io:8080/index.php", form);
		
		StartCoroutine( LoadWeb(www) );
		slide++;
	}
	
	IEnumerator LoadWeb(WWW www) {
		yield return www;
		
		if (www.error == null) {
			Debug.Log ("text:" + www.text);
			ParseJsonData(www.text);
			displayText.text = messageText;
		} else {
			Debug.Log("WWW Error: " + www.error);
			CloseStoryPanel();
		}
	}
	
	void ParseJsonData(string text) {
		JSONNode data = JSON.Parse(text);
		messageText     = data["dialogue"];
		//messageSpeaker  = data["speaker"];
	}
	
	public void Next() {
		LoadNextSlide();
	}
	
	public void OpenStoryPanel() {
		gameManager.PauseGame();
	}
	
	public void CloseStoryPanel() {
		gameObject.SetActive(false);
		gameManager.UnpauseGame();
	}
}
