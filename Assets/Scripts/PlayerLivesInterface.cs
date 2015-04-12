using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLivesInterface : MonoBehaviour {

	Text livesText;
	
	void Start () {
		GameObject obj = transform.Find("Lives Text").gameObject;
		livesText = obj.GetComponent<Text>();
		
		livesText.text = GameManager.currentLivesCount.ToString();
	}
	
	void Update () {
		livesText.text = GameManager.currentLivesCount.ToString();
	}
}
