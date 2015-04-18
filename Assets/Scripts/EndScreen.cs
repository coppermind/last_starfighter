using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreen : MonoBehaviour {

	GameObject finalScore;

	void Start () {
		finalScore = GameObject.Find("Final Score");
		
		if (finalScore) {
			Text text = finalScore.GetComponent<Text>();
			text.text = PlayerScore.TotalScore().ToString();
		}
	}
	
}
