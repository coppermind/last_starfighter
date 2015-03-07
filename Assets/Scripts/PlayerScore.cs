using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	private Text text;
	private int totalScore = 0;
	
	void Start () {
		text = GetComponent<Text>();
		text.text = totalScore.ToString();
	}
	
	void AddScore(int points) {
		totalScore += points;
		text.text = totalScore.ToString();
	}
}
