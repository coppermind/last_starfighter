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
		text.text = PlayerScore.totalScore.ToString();
	}
	
	public void AddScore(int points) {
		PlayerScore.totalScore += points;
		Debug.Log("Points: " + points);
		UpdateText();
	}
	
	public static void ResetScore() {
		PlayerScore.totalScore = 0;
	}
	
}
