using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	private static int totalScore = 0;
	
	private Text text;
	
	void Start () {
		text = GetComponent<Text>();
		UpdateText();
	}
	
	void UpdateText() {
		text.text = totalScore.ToString();
	}
	
	public void AddScore(int points) {
		totalScore += points;
		UpdateText();
	}
	
	public void ResetScore() {
		totalScore = 0;
	}
	
}
